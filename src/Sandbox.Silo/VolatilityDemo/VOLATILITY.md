# Why `VolatilityFailed.cs` doesn't actually demonstrate `volatile`

## What the demo was trying to show

`VolatilityFailed` has two fields:

```csharp
private volatile string name = "John";
private int age = 14;
```

The idea: `ThreadOne` waits 3 seconds, then updates both fields. `ThreadTwo` waits 5 seconds
(longer, so `ThreadOne` finishes first "behind the scenes"), then prints both fields. Because
`name` is `volatile` and `age` is not, the expectation was:

- `name` → always shows the fresh value (`"Harry"`), because `volatile` guarantees visibility.
- `age` → might show the stale, cached value (`14`), because a plain field has no such guarantee.

**What actually happens:** both fields show the fresh value, every time. `volatile` appears to
make no difference. This document explains why.

## First, what `volatile` actually is (and isn't)

This is the most common misconception, and it's the root of the confusion: `volatile` is **not**
a "flush my field's cache" switch that fixes an otherwise-persistent staleness problem. What it
actually does is much narrower — it tells the compiler and JIT:

1. Don't reorder this field's reads/writes relative to other reads/writes (a *reordering* fence).
2. Don't cache this field's value in a CPU register across multiple reads — always re-read it from
   memory (visibility of *your own* reads).

On x86/x64 (what almost everyone's desktop, laptop, and cloud VM runs on), the CPU's cache
coherency protocol already propagates a plain memory write to other cores quickly and
automatically — there is no long-lived "each core has its own stale copy forever" scenario at the
hardware level for ordinary loads and stores. The staleness that `volatile` guards against is
almost entirely a *compiler/JIT optimization* problem (the compiler deciding it can skip re-reading
a field because "nothing changes it, as far as I can prove"), not a hardware caching problem.

This matters because it means the bar for reproducing a visible difference is narrower than most
people expect: you need the JIT to actually have a reason to cache the read (e.g. a tight loop with
no other work in it), not just "some time passing between a write and a read on two different
threads."

## Why this specific demo can't show it

Even setting the above aside, this demo's structure guarantees both threads see fresh values
regardless of `volatile`, for two independent reasons:

### 1. `await`-ing a `Task` crosses several full memory fences

`ThreadTwo` doesn't read `name`/`age` in a tight loop — it reads them exactly once, right after
`await Task.Delay(5000)` resumes. That resumption is not a "free" continuation on the same
register state; it comes back through the `Task`/`ThreadPool`/timer machinery:

- The `Task.Delay` timer firing and completing the `Task` involves `Interlocked` operations and
  internal locking inside the `TaskCompletionSource`/timer queue.
- Scheduling the continuation back onto a `ThreadPool` thread goes through work-stealing queues
  (`ConcurrentQueue`-like structures) that use `Interlocked`/`Monitor` operations.
- Every one of those operations is a **full memory barrier**. Crossing a full barrier flushes and
  synchronizes *all* pending writes up to that point for that thread — not just writes to `volatile`
  fields.

So by the time `ThreadTwo`'s continuation runs, it has already crossed multiple full fences just to
get scheduled. Whatever `ThreadOne` wrote (`name` *and* `age`) becomes visible as a side effect of
that machinery, independent of the `volatile` keyword. `volatile` adds nothing here because the
`Task` infrastructure already did stricter synchronization than `volatile` provides.

### 2. `Console.WriteLine` is an optimization barrier

Even if the fences above didn't exist, the read happens as an argument to `Console.WriteLine(...)`,
a real method call the JIT cannot inline away or prove side-effect-free. The JIT cannot hoist a
field read out of, or cache it across, a call like that — so there's no opportunity for the classic
"the compiler cached the old value in a register and never re-read it" failure mode to occur here
in the first place. A single read-then-print is simply the wrong shape to expose the bug.

### 3. There's no loop to hoist a read out of

The other classic manifestation of missing `volatile` is a spin-wait loop (`while (!flag) { }`)
where the JIT lifts the field read out of the loop entirely, turning it into effectively
`if (!flag) { while (true) { } }` — an infinite loop, because the thread never re-reads memory.
`VolatilityFailed` never loops on the field at all; it reads each field exactly once. There's
nothing for the JIT to hoist, so there's no visible symptom to produce.

## Summary table

| Demo (`VolatilityFailed`)                          | Reality |
|-----------------------------------------------------|---------|
| Two threads with a `Task.Delay` gap between write and read | `Task`/`ThreadPool`/timer internals insert full memory fences on their own, flushing all pending writes regardless of `volatile` |
| Read happens inside `Console.WriteLine(...)`         | A method call the JIT can't optimize through — no register-caching opportunity exists |
| Field read once, not in a loop                        | Nothing for the JIT to hoist — the "stale forever" failure mode needs a loop to manifest |
| Conclusion                                             | This demo cannot show a difference between `volatile` and non-`volatile` *no matter how it's tuned* — the mechanism it relies on (elapsed wall-clock time between threads) isn't what `volatile` protects against |

## How `VolatilitySuccess.cs` fixes it

To actually observe the difference, the demo needs to:

1. Use raw `Thread`, not `Task`/`await` — avoids the `ThreadPool`/timer fences that mask the effect.
2. Spin-wait on the field in a tight loop (`while (!flag) { }`) — gives the JIT a reason to consider
   caching the read, and gives a hoisted read a way to manifest as an observable (near-)infinite loop.
3. Avoid method calls or other fences (locks, `Console.WriteLine`, etc.) *inside* the spin loop
   itself — only the loop condition should touch the field.

`VolatilitySuccess` runs the same experiment twice: once on a plain `bool` field, once on a
`volatile bool` field. With a plain field, a spinning reader thread can fail to ever observe the
writer thread's update (in this demo it gives up after a 5-second timeout, purely so the console
app doesn't hang forever — a genuine repro would spin indefinitely). With the `volatile` field, the
reader reliably observes the update almost immediately after the writer sets it.

### Caveats for running `VolatilitySuccess`

- **Run a Release build without a debugger attached** (`dotnet run -c Release`, or Ctrl+F5 in
  Visual Studio, not F5). A Debug build and/or an attached debugger both disable many JIT
  optimizations, including the exact one this demo relies on — you likely won't see the plain field
  ever get "stuck" under those conditions.
- **This is still not 100% guaranteed to hang**, even in Release. Modern .NET (tiered compilation,
  periodic GC safepoint polling, etc.) is stricter about memory visibility than the C# spec
  technically requires, so on some runtime versions/hardware the plain field may still eventually be
  observed as updated, just less reliably or more slowly than the `volatile` one. The takeaway isn't
  "the plain field is guaranteed to fail" — it's "only the `volatile` field is *guaranteed* to work;
  the plain field is relying on undefined behavior that happens to often work anyway."
 
 > I've tried running multiple times for the success demo, but it always able to observe the changes.
 > Volatile use-case remains fogged.
