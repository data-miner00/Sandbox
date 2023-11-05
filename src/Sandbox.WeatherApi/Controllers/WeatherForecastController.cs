namespace Sandbox.WeatherApi.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Sandbox.WeatherApi.Examples;
    using Swashbuckle.AspNetCore.Annotations;
    using Swashbuckle.AspNetCore.Filters;

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
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully retrieved info.")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Info not found.")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(WeatherForecastSingleExample))]
        [ProducesResponseType(typeof(WeatherForecastSingleExample), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await Task.FromResult(
                Enumerable.Range(1, 5).Select((index) => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                }).ToArray());

            return this.Ok(result);
        }
    }
}
