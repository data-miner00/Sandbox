namespace Sandbox.Aws
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;
    using Sandbox.Core.Events;

    internal class EventPublisher
    {
        private readonly AmazonSQSClient sqsClient;

        public EventPublisher(AmazonSQSClient sqsClient)
        {
            this.sqsClient = sqsClient;
        }

        public async Task PublishCustomerCreatedEvent(CustomerCreatedEvent @event)
        {
            // To get the url without hardcoding
            var queueUrlResponse = await this.sqsClient.GetQueueUrlAsync("customers");

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                MessageBody = JsonSerializer.Serialize(@event),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType = "String",
                            StringValue = nameof(CustomerCreatedEvent),
                        }
                    },
                },
                DelaySeconds = 3,
            };

            var response = await this.sqsClient.SendMessageAsync(sendMessageRequest);
        }
    }
}
