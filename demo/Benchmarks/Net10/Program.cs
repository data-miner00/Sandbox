using BenchmarkDotNet.Running;
using Benchmarks.Net10;

var runAllBenchmarks = false;

if (runAllBenchmarks)
{
    BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
}
else
{
    var summary = BenchmarkRunner.Run<SleepBenchmark>();
    var _ = summary;
}
