namespace Sandbox.Benchmarking
{
    using BenchmarkDotNet.Attributes;

    [MemoryDiagnoser]
    public class ConfigureAwait
    {
        [Params(100, 1_000, 2_000)]
        public int Size { get; set; }

        [Benchmark]
        public async Task WithoutConfigureAwaitFalse()
        {
            for (int i = 0; i < this.Size; i++)
            {
                await Task.Delay(1);
            }
        }

        [Benchmark]
        public async Task WithConfigureAwaitFalse()
        {
            for (int i = 0; i < this.Size; i++)
            {
                await Task.Delay(1).ConfigureAwait(false);
            }
        }
    }
}
