namespace Sandbox.Silo
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// A demo that can actually expose the difference between a <see langword="volatile"/> field and a
    /// plain field. See VOLATILITY.md in this folder for why <see cref="VolatilityFailed"/> could not.
    /// Run this in a Release build, without a debugger attached (Ctrl+F5, not F5) - JIT optimizations
    /// and the debugger both suppress the exact reordering/caching behavior being demonstrated.
    /// </summary>
    public class VolatilitySuccess
    {
        private volatile bool volatileReady;
        private bool plainReady;

        public void Demo()
        {
            Console.WriteLine("=== Plain field: a spin-wait reader may never see the writer's update ===");
            this.RunSpinWaitDemo(usePlainField: true);

            Console.WriteLine();
            Console.WriteLine("=== Volatile field: the spin-wait reader reliably sees the update ===");
            this.RunSpinWaitDemo(usePlainField: false);
        }

        private void RunSpinWaitDemo(bool usePlainField)
        {
            this.plainReady = false;
            this.volatileReady = false;
            var stopwatch = Stopwatch.StartNew();

            var writer = new Thread(() =>
            {
                Thread.Sleep(500);
                if (usePlainField)
                {
                    this.plainReady = true;
                }
                else
                {
                    this.volatileReady = true;
                }

                Console.WriteLine($"[writer] flag set to true at {stopwatch.ElapsedMilliseconds}ms");
            });

            var reader = new Thread(() =>
            {
                long spins = 0;
                while (usePlainField ? !this.plainReady : !this.volatileReady)
                {
                    spins++;

                    // Safety net for this demo only: without it, a reproduction of the stale-read
                    // behavior on the plain field would spin this thread forever.
                    if (stopwatch.ElapsedMilliseconds > 5000)
                    {
                        Console.WriteLine($"[reader] gave up after {spins:N0} spins ({stopwatch.ElapsedMilliseconds}ms) - the flag still looks false to this thread!");
                        return;
                    }
                }

                Console.WriteLine($"[reader] observed flag = true after {spins:N0} spins ({stopwatch.ElapsedMilliseconds}ms)");
            });

            reader.Start();
            writer.Start();
            reader.Join();
            writer.Join();
        }
    }
}
