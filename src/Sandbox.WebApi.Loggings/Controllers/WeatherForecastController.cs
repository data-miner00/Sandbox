namespace Sandbox.WebApi.Loggings.Controllers;

using Microsoft.AspNetCore.Mvc;
using Sandbox.WebApi.Loggings.Models;
using Serilog;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger _logger;

    public WeatherForecastController(ILogger logger)
    {
        _logger = logger;
    }

    [HttpGet("", Name = "GetWeatherForecast")]
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

    [HttpGet("unstable/{nonce:int?}", Name = "GetWeatherUnstable")]
    public IActionResult GetUnstable(int nonce = 20)
    {
        try
        {
            var random = Random.Shared.Next(100);

            if (random <= nonce)
            {
                throw new AbandonedMutexException();
            }

            var results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return this.Ok(results);
        }
        catch (Exception ex)
        {
            var referenceId = this.HttpContext.TraceIdentifier;

            this._logger.Error("[ERROR] <{referenceId}> {message}\n{stacktrace}", referenceId, ex.Message, ex.StackTrace);

            return this.BadRequest(new ErrorResponse
            {
                Message = "Something went wrong.",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ReferenceId = referenceId,
            });
        }
    }
}
