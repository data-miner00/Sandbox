namespace Sandbox.Aws.Services;

using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using MediatR;
using Microsoft.Extensions.Hosting;
using Sandbox.Aws.Events;
using Serilog;

/// <summary>
/// The SQS queue consumer service with extendable events.
/// </summary>
internal class QueueConsumerService : BackgroundService
{
    private readonly IAmazonSQS sqsClient;
    private readonly IMediator mediator;
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueueConsumerService"/> class.
    /// </summary>
    /// <param name="sqsClient">The <see cref="AmazonSQSClient"/> instance.</param>
    /// <param name="mediator">The <see cref="Mediator"/> instance.</param>
    /// <param name="logger">The <see cref="ILogger"/> implementation.</param>
    public QueueConsumerService(IAmazonSQS sqsClient, IMediator mediator, ILogger logger)
    {
        this.sqsClient = sqsClient;
        this.mediator = mediator;
        this.logger = logger;
    }

    /// <summary>
    /// Start listens to the queue and consume the events to their respective handlers.
    /// </summary>
    /// <param name="stoppingToken">The cancellation token.</param>
    /// <returns>Nothing.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrlResponse = await this.sqsClient.GetQueueUrlAsync("customers", stoppingToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MaxNumberOfMessages = 10,
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await this.sqsClient.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);

            foreach (var message in response.Messages)
            {
                var messageType = message.MessageAttributes["MessageType"].StringValue;
                var type = Type.GetType($"Sandbox.Core.Events.{messageType}");

                if (type is null)
                {
                    this.logger.Warning("Unknown message type: {MessageType}", messageType);
                    continue;
                }

                var typedMessage = (ISqsMessage)JsonSerializer.Deserialize(message.Body, type)!;

                try
                {
                    await this.mediator.Send(typedMessage, stoppingToken);
                }
                catch (Exception ex)
                {
                    this.logger.Error(ex, "Message failed during processing");
                    continue;
                }

                await this.sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle, stoppingToken);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
