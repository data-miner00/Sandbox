using BenchmarkDotNet.Running;
using Benchmarks.Net10;

var summary = BenchmarkRunner.Run<MultipleWhere>();
var _ = summary;
