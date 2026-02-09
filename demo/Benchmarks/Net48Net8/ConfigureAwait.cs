namespace Benchmarks.Net48Net8;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Threading.Tasks;

[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[MemoryDiagnoser]
public class ConfigureAwait
{
    [Params(100, 200)]
    public int Size { get; set; }

    [Benchmark(Baseline = true)]
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
