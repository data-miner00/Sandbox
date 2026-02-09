using BenchmarkDotNet.Running;
using Benchmarks.Net48Net8;

var summary = BenchmarkRunner.Run<ConfigureAwait>();
