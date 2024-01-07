// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using Sandbox.Benchmarking;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<SumOdd>();
    }
}
