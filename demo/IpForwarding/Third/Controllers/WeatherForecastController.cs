using Microsoft.AspNetCore.Mvc;

namespace Third.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        public record WeatherForecastResponse(string ForwardedIps, IEnumerable<WeatherForecast> WeatherForecasts);

        [HttpGet(Name = "GetWeatherForecast")]
        public WeatherForecastResponse Get()
        {
            var weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });

            if (this.HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var value))
            {
                return new WeatherForecastResponse(value!, weatherForecasts);
            }

            return new WeatherForecastResponse("No IP Forwarded.", weatherForecasts);
        }
    }
}
