using Microsoft.AspNetCore.Mvc;
using Polly;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, context) =>
                    {
                        _logger.LogError(exception, "An error occurred while processing your request. Retrying in {TimeSpan} seconds.", timeSpan.TotalSeconds);
                    });

            var forecast = await retryPolicy.ExecuteAsync(async () =>
            {
                var isSuccess = await Task.FromResult(true);
                if (isSuccess) // Simulate a condition that might throw an exception
                {
                    throw new Exception("Simulated exception for demonstration purposes.");
                }

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            });

            return forecast ?? [];
        }

        [HttpGet(Name = "GetWeatherForecast2")]
        public async Task<IEnumerable<WeatherForecast>> Get2()
        {
            var retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, context) =>
                    {
                        _logger.LogError(exception, "An error occurred while processing your request. Retrying in {TimeSpan} seconds.", timeSpan.TotalSeconds);
                    });

            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30),
                    (exception, duration) =>
                    {
                        _logger.LogWarning(exception, "Circuit breaker opened for {Duration} seconds.", duration.TotalSeconds);
                    },
                    () => _logger.LogInformation("Circuit breaker closed."));

            var policy = retryPolicy.WrapAsync(circuitBreakerPolicy);

            var forecast = await policy.ExecuteAsync(async () =>
            {
                var isSuccess = await Task.FromResult(true);
                if (isSuccess) // Simulate a condition that might throw an exception
                {
                    throw new Exception("Simulated exception for demonstration purposes.");
                }

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            });

            return forecast ?? [];
        }
    }
}
