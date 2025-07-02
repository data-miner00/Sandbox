namespace Sandbox.Azure.Functions
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    public class Recurring
    {
        [FunctionName("Recurring")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
