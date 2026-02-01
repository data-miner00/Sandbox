using BenchmarkDotNet.Running;
using Benchmarks.Net8Net10;

var summary = BenchmarkRunner.Run<MultipleWhere>();
