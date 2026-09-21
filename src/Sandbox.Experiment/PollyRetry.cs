namespace Sandbox.Experiment;

using System;
using Polly;
using Polly.Retry;

internal static class PollyRetry
{
    /// <summary>
    /// Proofs that polly resillience pipeline encapsulates the retry state for each individual `ExecuteAsync` rather than
    /// sharing state at the pipeline level.
    /// </summary>
    /// <returns>Empty task.</returns>
    /// <exception cref="Exception">A simulated exception.</exception>
    public static async Task DemoAsync()
    {
        var options = new RetryStrategyOptions
        {
            MaxRetryAttempts = 5,
            UseJitter = true,
            BackoffType = DelayBackoffType.Exponential,
        };

        var pipeline = new ResiliencePipelineBuilder()
            .AddRetry(options)
            .AddTimeout(TimeSpan.FromSeconds(10))
            .Build();

        const int TotalTasks = 10;
        int actualCalls = 0;

        var tasks = Enumerable.Range(0, TotalTasks).Select(_ => ExecuteOneAsync());

        await Task.WhenAll(tasks);

        Console.WriteLine($"Actual count: {actualCalls}");

        async Task ExecuteOneAsync()
        {
            await pipeline.ExecuteAsync(async (ct) =>
            {
                Interlocked.Increment(ref actualCalls);
                if (Random.Shared.Next(5) <= 2)
                {
                    throw new Exception("Simulated exception.");
                }

                Console.WriteLine("Done.");
            });
        }
    }
}
