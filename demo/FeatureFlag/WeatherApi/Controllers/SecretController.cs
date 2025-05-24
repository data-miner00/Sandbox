namespace WeatherApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

[ApiController]
[Route("[controller]")]
[FeatureGate("EnableSecretFeature")]
public class SecretController : ControllerBase
{
    [HttpGet]
    public IActionResult ExperimentalFeature()
    {
        return this.Ok("Experimental feature is enabled for you");
    }
}
