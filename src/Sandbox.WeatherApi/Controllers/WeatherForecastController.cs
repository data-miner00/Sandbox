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
        [SwaggerResponse((int)HttpStatusCode.OK, "Weather retrieved successfully.")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(WeatherForecastExample))]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            return this.Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}
