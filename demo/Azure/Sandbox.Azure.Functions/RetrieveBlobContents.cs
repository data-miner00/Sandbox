namespace Sandbox.Azure.Functions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;

    public static class RetrieveBlobContents
    {
        [FunctionName("RetrieveBlobContents")]
        public static ActionResult Run(
            [HttpTrigger("GET")] HttpRequest req,
            [Blob("content/settings.json")] string settings)
        {
            return new OkObjectResult(settings);
        }
    }
}
