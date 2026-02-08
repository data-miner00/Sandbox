namespace Sandbox.WeatherApi.Controllers
{
    using System.Net;

    using Microsoft.AspNetCore.Mvc;

    using Polly.Registry;

    using Sandbox.WeatherApi.Examples;
    using Sandbox.WeatherApi.Filters;
    using Sandbox.WeatherApi.Models;

    using Swashbuckle.AspNetCore.Annotations;
    using Swashbuckle.AspNetCore.Filters;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> logger;
        private readonly ResiliencePipelineProvider<string> pipelineProvider;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ResiliencePipelineProvider<string> pipelineProvider)
        {
            this.logger = logger;
            this.pipelineProvider = pipelineProvider;
            logger.LogInformation("Hello world");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully retrieved info.")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Info not found.")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(WeatherForecastSingleExample))]
        [ProducesResponseType(typeof(WeatherForecastSingleExample), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var pipeline = this.pipelineProvider.GetPipeline("default");

            var result = await pipeline.ExecuteAsync(async (cancellationToken) =>
            {
                var randomInt = Random.Shared.Next(1, 100);

                if (randomInt < 50)
                {
                    // Simulate a failure
                    this.logger.LogError("Random failure occurred with value {RandomInt}", randomInt);
                    throw new Exception("Random failure occurred.");
                }

                return await Task.FromResult(
                Enumerable.Range(1, 5).Select((index) => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                }).ToArray());
            });

            return this.Ok(result);
        }

        [HttpGet("random", Name = "Random")]
        [ScopedFilter("hello world")]
        [ScopedAsyncFilter]
        public Task<IActionResult> GetRandom()
        {
            return Task.FromResult(this.Ok(20) as IActionResult);
        }

        [HttpPost("echo/file")]
        public async Task<IActionResult> EchoFileContent(IFormFile file)
        {
            string fileContents;
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                fileContents = await reader.ReadToEndAsync();
            }

            return this.Ok(fileContents);
        }
    }
}
