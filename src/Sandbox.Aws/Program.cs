namespace Sandbox.Aws
{
    using Amazon;
    using Amazon.SQS;

    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            // many ways to instantiate a sqs client
            var sqsClient = new AmazonSQSClient(RegionEndpoint.AFSouth1);

            var sqsClient2 = new AmazonSQSClient(new AmazonSQSConfig
            {
                RegionEndpoint = RegionEndpoint.APNortheast1,
            });
        }
    }
}
