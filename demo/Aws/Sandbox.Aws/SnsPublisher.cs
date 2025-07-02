namespace Sandbox.Aws;

using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Sandbox.Aws.Events;

/// <summary>
/// The publisher class to Amazon Simple Notification Service.
/// </summary>
internal class SnsPublisher
{
    private readonly IAmazonSimpleNotificationService snsClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="SnsPublisher"/> class.
    /// </summary>
    /// <param name="snsClient">The <see cref="AmazonSimpleNotificationServiceClient"/>.</param>
    public SnsPublisher(IAmazonSimpleNotificationService snsClient)
    {
        this.snsClient = snsClient;
    }

    /// <summary>
    /// Publishes a customer created event to the Sns.
    /// </summary>
    /// <param name="event">The info for the <see cref="CustomerCreatedEvent"/>.</param>
    /// <returns>Nothing.</returns>
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
