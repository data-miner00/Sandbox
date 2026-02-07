using BenchmarkDotNet.Running;
using Benchmarks.Net10;

var summary = BenchmarkRunner.Run<JoinArrayBenchmark>();
var _ = summary;
