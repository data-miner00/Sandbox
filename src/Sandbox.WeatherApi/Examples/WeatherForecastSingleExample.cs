namespace Sandbox.WeatherApi.Examples
{
    using System.Collections.Generic;
    using Swashbuckle.AspNetCore.Filters;

    public class WeatherForecastSingleExample : IExamplesProvider<IEnumerable<WeatherForecast>>
    {
        public IEnumerable<WeatherForecast> GetExamples()
        {
            return new[]
            {
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "Chilly",
                    TemperatureC = 32,
                },
                new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "Sweltering",
                    TemperatureC = 40,
                },
            };
        }
    }
}
