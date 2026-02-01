namespace Benchmarks.Net48Net10
{
    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MultipleWhere>();
        }
    }
}
