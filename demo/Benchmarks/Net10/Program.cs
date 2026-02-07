using BenchmarkDotNet.Running;
using Benchmarks.Net10;

var summary = BenchmarkRunner.Run<SleepBenchmark>();
var _ = summary;
