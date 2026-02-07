namespace Benchmarks.Net10;

using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

[MemoryDiagnoser]
public class SleepBenchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(GetSleepDurations))]
    public string ThreadSleep(int ms)
    {
        Thread.Sleep(ms);

        return "Prevent JIT Elimination";
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetSleepDurations))]
    public async Task<string> TaskSleep(int ms)
    {
        await Task.Delay(ms);

        return "Prevent JIT Elimination";
    }

    public IEnumerable<int> GetSleepDurations()
    {
        return [1, 5, 15];
    }
}
