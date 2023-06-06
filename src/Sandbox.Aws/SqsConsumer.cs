namespace Sandbox.Aws
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;

    internal class SqsConsumer
    {
        private readonly IAmazonSQS sqsClient;

        public SqsConsumer(IAmazonSQS sqsClient)
        {
            this.sqsClient = sqsClient;
        }

        public async Task ConsumeCustomerCreatedEvent()
        {
            var cts = new CancellationTokenSource();
            var queueUrlResponse = await this.sqsClient.GetQueueUrlAsync("customers");

            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,

                // By default, these values are excluded to be efficient
                AttributeNames = new List<string> { "All" },
                MessageAttributeNames = new List<string> { "All" },
            };

            while (!cts.IsCancellationRequested)
            {
                var response = await this.sqsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);

                // The messages are not deleted by default, needs to explicitly mark them as delete.
                foreach (var message in response.Messages)
                {
                    Console.WriteLine($"Message Id: {message.MessageId}");
                    Console.WriteLine($"Message Body: {message.Body}");

                    await this.sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
                }

                await Task.Delay(3000);
            }
        }
    }
}
