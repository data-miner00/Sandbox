using ConsoleApp;
using Polly;
using Polly.Retry;
using Serilog;

var logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var errorRate = 60;

var client = new DangerClient(errorRate, logger);

// Use simple policy
var policy = Policy.Handle<BarrierPostPhaseException>()
    .Retry(10);

policy.Execute(() =>
{
    client.Execute();
});
logger.Information("Congratulations, the program ended in a success.");

// Use resilience pipeline and retry strategy
var options = new RetryStrategyOptions
{
    ShouldHandle = new PredicateBuilder().Handle<BarrierPostPhaseException>(),
    BackoffType = DelayBackoffType.Exponential,
    UseJitter = true,  // Adds a random factor to the delay
    MaxRetryAttempts = 10,
    Delay = TimeSpan.FromSeconds(3),
};

var pipelineBuilder = new ResiliencePipelineBuilder().AddRetry(options);

client = new DangerClient(errorRate, logger);
pipelineBuilder.Build().Execute(() =>
{
    client.Execute();
});

logger.Information("Congratulations, the program ended in a success.");
