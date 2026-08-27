namespace Sandbox.Silo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Intended to demonstrate that a <see langword="volatile"/> field (<see cref="name"/>) is always
    /// visible fresh across threads while a plain field (<see cref="age"/>) can be observed stale.
    /// It does not actually demonstrate that - see VOLATILITY.md in this folder for why.
    /// </summary>
    public class VolatilityFailed
    {
        private volatile string name = "John";
        private int age = 14;

        public async Task DemoAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            /*
             * |     thread two     |
             *    | thread one |
             * While thread two (longer time to process) is processing, thread one is completed behind the scene.
             */
            Console.WriteLine("Thread Id in main is {0}", Environment.CurrentManagedThreadId);
            var task2 = this.ThreadTwo().ConfigureAwait(false);

            _ = this.ThreadOne().ConfigureAwait(false);

            await task2;
            stopwatch.Stop();

            Console.WriteLine($"Process ended in {stopwatch.ElapsedMilliseconds}ms");
        }

        public Task ThreadOne()
        {
            return Task.Run(async () =>
            {
                Console.WriteLine("Thread Id {0}", Environment.CurrentManagedThreadId);
                await Task.Delay(3000);
                this.name = "Harry";
                this.age = 11;
            });
        }

        public Task ThreadTwo()
        {
            return Task.Run(async () =>
            {
                Console.WriteLine("Thread Id {0}", Environment.CurrentManagedThreadId);
                await Task.Delay(5000);
                Console.WriteLine($"The latest name is: {this.name}. The outdated age is: {this.age}");
            });
        }
    }
}
