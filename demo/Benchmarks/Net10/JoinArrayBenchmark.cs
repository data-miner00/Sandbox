namespace Benchmarks.Net10;

using BenchmarkDotNet.Attributes;
using Benchmarks.Core.Net10;

[MemoryDiagnoser]
public class JoinArrayBenchmark
{
    public int[] array = [1, 2, 3, 4, 5];

    [Benchmark]
    public string JoinArray1()
    {
        return JoinArray.JoinArray1(array);
    }

    [Benchmark]
    public string JoinArray2()
    {
        return JoinArray.JoinArray2(array);
    }

    [Benchmark]
    public string JoinArray3()
    {
        return JoinArray.JoinArray3(array);
    }
}
