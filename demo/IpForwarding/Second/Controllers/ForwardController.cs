using Core;
using Microsoft.AspNetCore.Mvc;

namespace Second.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForwardController : ControllerBase
    {
        private readonly HttpClient httpClient;

        public ForwardController(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient(Constants.ThirdApi);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var content = await this.httpClient.GetStringAsync("weatherforecast");
            return Ok(content);
        }
    }
}
