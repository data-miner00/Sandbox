namespace Sandbox.Silo
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Demos for <see cref="TaskCompletionSource{TResult}"/> (and the non-generic <see cref="TaskCompletionSource"/>
    /// added in .NET 5): the standard tool for handing back an awaitable <see cref="Task"/> for work whose
    /// completion is driven by something other than another <c>Task</c> - a callback, an event, a message
    /// arriving on a socket, an IO completion port. See TASK_COMPLETION_SOURCE.md in this folder for what
    /// problem it solves, why <see cref="TaskCreationOptions.RunContinuationsAsynchronously"/> should (almost)
    /// always be passed, Try* vs non-Try* completion, and other gotchas.
    /// See also: https://www.pluralsight.com/resources/blog/guides/task-taskcompletion-source-csharp.
    /// </summary>
    public class TaskCompletionSourceDemo
    {
        public async Task Demo()
        {
            await this.WrappingALegacyCallbackApiAsync();
            await this.TrySetVsSetAsync();
            await this.RunContinuationsAsynchronouslyPitfallAsync();
            await this.NonGenericSignalAsync();
            await this.TimeoutViaCancellationTokenAsync();
        }

        // 1. The textbook use case: adapt a callback-based ("legacy", event-based, IO-completion-based) API
        // into something a caller can simply await.
        private async Task WrappingALegacyCallbackApiAsync()
        {
            Console.WriteLine("=== 1. Wrapping a callback API with TaskCompletionSource<T> ===");

            var result = await RunLegacyOperationAsync(21);

            Console.WriteLine($"Legacy callback API produced: {result}");
            Console.WriteLine();

            static Task<int> RunLegacyOperationAsync(int input)
            {
                // Always specify RunContinuationsAsynchronously when creating a TaskCompletionSource that a
                // caller will await - see demo 3 below for exactly what goes wrong if you don't.
                var tcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

                LegacyOperationWithCallback(
                    input,
                    onSuccess: value => tcs.TrySetResult(value),
                    onError: ex => tcs.TrySetException(ex));

                return tcs.Task;
            }

            static void LegacyOperationWithCallback(int input, Action<int> onSuccess, Action<Exception> onError)
            {
                // Stand-in for an old-style API (an event, a delegate parameter, an IO completion port...)
                // that only knows how to report completion via a callback, on its own thread, at its own
                // pace - there is no Task anywhere in its surface area.
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Thread.Sleep(200);
                    onSuccess(input * 2);
                });
            }
        }

        // 2. SetResult throws if the task is already completed; TrySetResult reports false instead. Prefer
        // Try* whenever more than one place could race to complete the same TaskCompletionSource - e.g. a
        // real result racing a timeout or a cancellation (see demo 5).
        private async Task TrySetVsSetAsync()
        {
            Console.WriteLine("=== 2. SetResult vs TrySetResult ===");

            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            var firstAttempt = tcs.TrySetResult("first");
            var secondAttempt = tcs.TrySetResult("second"); // already completed - ignored, returns false

            Console.WriteLine($"First TrySetResult succeeded: {firstAttempt}");
            Console.WriteLine($"Second TrySetResult succeeded: {secondAttempt}");
            Console.WriteLine($"Winning result: {await tcs.Task}");

            try
            {
                tcs.SetResult("third"); // SetResult throws because the task is already completed
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"SetResult on an already-completed task threw: {ex.GetType().Name}");
            }

            Console.WriteLine();
        }

        // 3. Without RunContinuationsAsynchronously, continuations attached to tcs.Task (via ContinueWith or
        // await) run synchronously, inline, on whatever thread calls SetResult/SetException/SetCanceled -
        // including all of the caller's downstream logic. That can turn a producer thread (an IO completion
        // thread, a UI thread, a lock-holding thread) into an accidental execution context for arbitrary
        // consumer code: deadlocks, stack dives on nested completions, or code running somewhere the producer
        // never intended. This is documented, deterministic TPL behavior - not a timing-dependent race.
        private async Task RunContinuationsAsynchronouslyPitfallAsync()
        {
            Console.WriteLine("=== 3. TaskCreationOptions.RunContinuationsAsynchronously ===");

            var unsafeTcs = new TaskCompletionSource<int>();
            var safeTcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

            var unsafeContinuationThreadId = -1;
            var safeContinuationThreadId = -1;

            // ExecuteSynchronously just makes the continuation *eligible* for inlining - whether it actually
            // gets inlined is still decided by the antecedent's RunContinuationsAsynchronously option below.
            unsafeTcs.Task.ContinueWith(
                _ => unsafeContinuationThreadId = Environment.CurrentManagedThreadId,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);
            safeTcs.Task.ContinueWith(
                _ => safeContinuationThreadId = Environment.CurrentManagedThreadId,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);

            var settingThreadId = Environment.CurrentManagedThreadId;
            Console.WriteLine($"Calling SetResult on thread {settingThreadId} for both");

            unsafeTcs.SetResult(1); // default options: the continuation above runs inline, before SetResult even returns
            safeTcs.SetResult(1);   // RunContinuationsAsynchronously: the continuation is queued to the thread pool instead

            Console.WriteLine(
                $"Immediately after SetResult - unsafe continuation already ran on thread: " +
                $"{(unsafeContinuationThreadId == -1 ? "no (still -1)" : unsafeContinuationThreadId.ToString())}, " +
                $"safe continuation already ran: {(safeContinuationThreadId == -1 ? "no (still -1)" : safeContinuationThreadId.ToString())}");

            await Task.Delay(100); // give the thread pool a moment to run the "safe" continuation

            Console.WriteLine($"unsafeTcs continuation ran on thread {unsafeContinuationThreadId} (same thread as SetResult caller: {unsafeContinuationThreadId == settingThreadId})");
            Console.WriteLine($"safeTcs continuation ran on thread {safeContinuationThreadId} (same thread as SetResult caller: {safeContinuationThreadId == settingThreadId})");
            Console.WriteLine();
        }

        // 4. The non-generic TaskCompletionSource (.NET 5+) is for when there's no result value, just a
        // "has this happened yet" signal. It's the async-friendly analogue of ManualResetEventSlim: awaiting
        // it doesn't block a thread pool thread, where ManualResetEventSlim.Wait() would.
        private async Task NonGenericSignalAsync()
        {
            Console.WriteLine("=== 4. Non-generic TaskCompletionSource as an async 'gate' ===");

            var gate = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

            var waiter = Task.Run(async () =>
            {
                Console.WriteLine("Waiter: waiting for the gate to open...");
                await gate.Task;
                Console.WriteLine("Waiter: gate opened, continuing.");
            });

            await Task.Delay(200);
            Console.WriteLine("Main: opening the gate.");
            gate.SetResult();

            await waiter;
            Console.WriteLine();
        }

        // 5. The standard pattern for giving a pending, externally-completed operation a timeout: race the
        // "real" completion against a CancellationTokenSource-driven one on the same TaskCompletionSource,
        // using TrySet* so whichever fires first wins and the loser is silently ignored.
        private async Task TimeoutViaCancellationTokenAsync()
        {
            Console.WriteLine("=== 5. Combining TaskCompletionSource with a timeout ===");

            Console.WriteLine("-- operation completes before the timeout --");
            await RunWithTimeoutAsync(operationDelay: TimeSpan.FromMilliseconds(100), timeout: TimeSpan.FromSeconds(1));

            Console.WriteLine("-- operation is slower than the timeout --");
            await RunWithTimeoutAsync(operationDelay: TimeSpan.FromSeconds(2), timeout: TimeSpan.FromMilliseconds(200));

            Console.WriteLine();

            static async Task RunWithTimeoutAsync(TimeSpan operationDelay, TimeSpan timeout)
            {
                var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

                using var cts = new CancellationTokenSource(timeout);
                using var registration = cts.Token.Register(() => tcs.TrySetCanceled(cts.Token));

                // Stand-in for the "real" callback-based operation from demo 1, just slower and configurable.
                _ = Task.Run(async () =>
                {
                    await Task.Delay(operationDelay);
                    tcs.TrySetResult("operation result");
                });

                try
                {
                    var result = await tcs.Task;
                    Console.WriteLine($"Completed in time: {result}");
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine($"Timed out after {timeout.TotalMilliseconds}ms");
                }
            }
        }
    }
}
