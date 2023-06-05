namespace Sandbox.Aws.Services
{
    using System;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using Amazon.SQS.Model;
    using MediatR;
    using Microsoft.Extensions.Hosting;
    using Sandbox.Core.Events;
    using Serilog;

    internal class QueueConsumerService : BackgroundService
    {
        private readonly IAmazonSQS sqsClient;
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public QueueConsumerService(IAmazonSQS sqsClient, IMediator mediator, ILogger logger)
        {
            this.sqsClient = sqsClient;
            this.mediator = mediator;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueUrlResponse = await this.sqsClient.GetQueueUrlAsync("customers");

            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                MaxNumberOfMessages = 1,
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
}
