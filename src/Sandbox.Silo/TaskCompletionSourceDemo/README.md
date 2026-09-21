# `TaskCompletionSource<T>`: what it's for, and how not to misuse it

## The problem it solves

`async`/`await` is great when the work you're wrapping is *already* a `Task` - you `await` it, or
chain it with `Task.Run`. But sometimes the thing you need to expose as a `Task` isn't driven by
another `Task` at all:

- a callback-based ("APM"/"legacy") API that reports completion via a delegate parameter
- an event (`SomeClient.MessageReceived += ...`)
- a message arriving on a socket, a message queue, a SignalR hub
- a hardware or IO completion port
- a manual "gate" one part of your code opens and another part waits on

`TaskCompletionSource<TResult>` (and, since .NET 5, the non-generic `TaskCompletionSource` for
"no result, just a signal") is the adapter: you create one, hand its `.Task` to callers to
`await`, and call `SetResult`/`SetException`/`SetCanceled` (or the `Try*` variants) yourself,
whenever *you* decide the operation is done. It is **not** related to `Task.Run` - creating a
`TaskCompletionSource` does not start any work, run anything on the thread pool, or block
anything. It's just a box that produces a `Task` you can complete by hand.

Rule of thumb: if the work you're wrapping is already `async`, just write an `async` method and
`await` inside it. Reach for `TaskCompletionSource` only at the boundary where an external,
non-`Task` completion mechanism needs to become a `Task`.

## Best practice #1: always pass `TaskCreationOptions.RunContinuationsAsynchronously`

This is the single most important thing to know about `TaskCompletionSource`, and it's opt-in for
historical/back-compat reasons rather than being the default:

```csharp
var tcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
```

Without it, everything chained onto `tcs.Task` - a `.ContinueWith`, or the code after an `await
tcs.Task` - runs **synchronously, inline, on whatever thread calls `SetResult`** (see demo 3 in
`TaskCompletionSourceDemo.cs`, which shows this directly by comparing thread IDs). This is not a
timing-dependent race the way the `volatile` demo in this project is; it's documented, guaranteed
TPL behavior.

Why that's dangerous:

- **You don't control what thread your completion runs the caller's code on.** If `SetResult` is
  called from inside a lock, from a UI thread, from an IO completion thread, or from code that
  itself expects to run quickly and return, the *caller's* continuation - which could be arbitrary
  application logic - now runs there too, synchronously, before your `SetResult` call returns.
- **Deadlocks.** If the awaiting caller's continuation tries to re-acquire a lock the producer
  thread is still holding (because it hasn't returned from `SetResult` yet), you deadlock.
- **Stack dives.** A chain of TCS-backed operations that each complete the next one from inside
  their own completion can build up an unbounded synchronous call stack instead of the usual
  trampolined `async` continuation chain, occasionally ending in a `StackOverflowException`.

`RunContinuationsAsynchronously` fixes this by guaranteeing the continuation is queued to the
thread pool instead of run inline - exactly what you'd expect from `await` in the first place.
There is essentially never a good reason to omit it for a `TaskCompletionSource` whose `.Task` an
external caller will `await`.

## Best practice #2: prefer `Try*` over the throwing `Set*` methods

- `SetResult`/`SetException`/`SetCanceled` **throw `InvalidOperationException`** if the task is
  already completed.
- `TrySetResult`/`TrySetException`/`TrySetCanceled` instead just **return `false`** and no-op.

Use the throwing versions only when completing the source exactly once is a genuine invariant you
want enforced loudly (a bug if violated). Use `Try*` whenever more than one code path could race
to complete the same source - the classic case being a real result racing a timeout or a
cancellation (demo 5): whichever fires first "wins" via `TrySet*`, and the loser's call is simply
ignored instead of throwing and crashing an unrelated callback.

## Best practice #3: propagate errors and cancellation, not just results

A callback-based API usually has an error channel (an `onError` callback, an `Exception` passed to
a completion delegate, a `Faulted`-style event). Map that onto `TrySetException`, so that awaiting
`tcs.Task` throws the *original* exception the way any other faulted `Task` would - callers get
normal `try`/`catch`/`await` semantics instead of having to know your API has its own bespoke error
reporting.

Similarly, if the operation supports cancellation, complete the source with `TrySetCanceled` (see
demo 5's `CancellationToken.Register` pattern) rather than a made-up sentinel value or exception
type - awaiting a canceled `Task` throws `TaskCanceledException`/`OperationCanceledException`,
which is what calling code already knows how to handle.

## Other gotchas

- **A `TaskCompletionSource` that's never completed is a Task that never completes - and a leak.**
  Anything awaiting it waits forever, and its continuation chain (and anything it's holding a
  reference to, transitively) is pinned in memory. Always make sure every code path - success,
  error, and cancellation/timeout - eventually calls some `Set*`/`Try*` method.
- **The non-generic `TaskCompletionSource`** (.NET 5+, demo 4) exists so you don't have to write
  `TaskCompletionSource<object?>` and `SetResult(null)` just to signal "done, no value" - use it
  for gate/signal scenarios (an async alternative to `ManualResetEventSlim` that doesn't block a
  thread while waiting).
- **`TaskCreationOptions` on a `TaskCompletionSource` is a small, different subset from the general
  `TaskCreationOptions` enum** - only `None`, `RunContinuationsAsynchronously`, `AttachedToParent`,
  and `DenyChildAttach` are meaningful here; options like `LongRunning` don't apply because no
  actual work is being scheduled by the source itself.
