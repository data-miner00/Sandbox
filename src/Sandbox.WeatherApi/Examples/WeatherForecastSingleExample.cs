namespace Sandbox.WeatherApi.Examples
{
    using Swashbuckle.AspNetCore.Filters;

    public class WeatherForecastSingleExample : IExamplesProvider<WeatherForecast>
    {
        public WeatherForecast GetExamples()
        {
            return new WeatherForecast
            {
                Date = DateTime.Now,
                Summary = "Chilly",
                TemperatureC = 32,
            };
        }
    }
}
