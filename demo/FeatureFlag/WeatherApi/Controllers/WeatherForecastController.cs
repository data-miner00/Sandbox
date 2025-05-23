using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IFeatureManager featureManager;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IFeatureManager featureManager)
    {
        _logger = logger;
        this.featureManager = featureManager;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("experimental")]
    public async Task<IActionResult> ExperimentalFeature()
    {
        var isEnabled = await featureManager.IsEnabledAsync("EnableExperimentalFeature");

        if (!isEnabled)
        {
            return this.NotFound();
        }

        return this.Ok("Experimental feature is enabled");
    }
}
