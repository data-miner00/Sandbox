namespace Sandbox.WeatherApi.Examples
{
    using Swashbuckle.AspNetCore.Filters;
    using System.Collections.Generic;

    public class WeatherForecastExample : IMultipleExamplesProvider<WeatherForecast>
    {
        public IEnumerable<SwaggerExample<WeatherForecast>> GetExamples()
        {
            yield return SwaggerExample.Create(
                "Example 1",
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "Chilly",
                    TemperatureC = 32,
                });

            yield return SwaggerExample.Create(
                "Example 2",
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "Warm",
                    TemperatureC = 48,
                });
        }
    }
}
