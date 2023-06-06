namespace Sandbox.Aws
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Amazon.SimpleNotificationService;
    using Amazon.SimpleNotificationService.Model;
    using Sandbox.Core.Events;

    internal class SnsPublisher
    {
        private readonly IAmazonSimpleNotificationService snsClient;

        public SnsPublisher(IAmazonSimpleNotificationService snsClient)
        {
            this.snsClient = snsClient;
        }

        public async Task PublishCustomerCreatedEvent(CustomerCreatedEvent @event)
        {
            var topicArnResponse = await this.snsClient.FindTopicAsync("customers");
            var publishRequest = new PublishRequest
            {
                TopicArn = topicArnResponse.TopicArn,
                Message = JsonSerializer.Serialize(@event),
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
            };

            var response = await this.snsClient.PublishAsync(publishRequest);
        }
    }
}
