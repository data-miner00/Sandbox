namespace Sandbox.Experiment
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Semaphora
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(5);
        private long activeCount = 0;

        public static Task DemoAsync()
        {
            var internalClass = new Semaphora();
            var tasks = Enumerable.Range(0, 50).Select(_ => internalClass.AccessResourceAsync());
            return Task.WhenAll(tasks);
        }

        public async Task AccessResourceAsync()
        {
            await this.semaphore.WaitAsync();
            try
            {
                Interlocked.Increment(ref this.activeCount);

                // Simulate resource access
                Console.WriteLine("Accessing resource...Current active tasks: {0}", Interlocked.Read(ref this.activeCount));
                await Task.Delay(1000 * Random.Shared.Next(5));
            }
            finally
            {
                this.semaphore.Release();
                Interlocked.Decrement(ref this.activeCount);
            }
        }
    }
}
