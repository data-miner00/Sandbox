using Core;
using Microsoft.AspNetCore.Mvc;

namespace First.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForwardController : ControllerBase
    {
        private readonly HttpClient httpClient;

        public ForwardController(IHttpClientFactory factory)
        {
            this.httpClient = factory.CreateClient(Constants.SecondApi);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var content = await this.httpClient.GetStringAsync("forward");
            return Ok(content);
        }
    }
}
