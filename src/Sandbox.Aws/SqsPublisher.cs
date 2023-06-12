namespace Sandbox.Aws;

using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Sandbox.Core.Events;

/// <summary>
/// The event publisher to the Simple Queue System.
/// </summary>
internal class SqsPublisher
{
    private readonly IAmazonSQS sqsClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqsPublisher"/> class.
    /// </summary>
    /// <param name="sqsClient">The <see cref="AmazonSQSClient"/> instance.</param>
    public SqsPublisher(IAmazonSQS sqsClient)
    {
        this.sqsClient = sqsClient;
    }

    /// <summary>
    /// Publishes the customer created event.
    /// </summary>
    /// <param name="event">The <see cref="CustomerCreatedEvent"/>.</param>
    /// <returns>Nothing.</returns>
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
