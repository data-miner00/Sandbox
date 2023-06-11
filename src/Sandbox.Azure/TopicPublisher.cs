namespace Sandbox.Azure
{
    using System;
    using System.Threading.Tasks;
    using global::Azure;
    using global::Azure.Messaging.EventGrid;

    internal class TopicPublisher
    {
        private readonly EventGridPublisherClient client;

        public TopicPublisher(string topicEndpoint, string topicKey)
        {
            var credential = new AzureKeyCredential(topicKey);
            this.client = new EventGridPublisherClient(new Uri(topicEndpoint), credential);
        }

        public async Task PublishTopic()
        {
            var @event = new EventGridEvent(
                subject: $"New Employee: Alba Sutton",
                eventType: "Employees.Registration.New",
                dataVersion: "1.0",
                data: new
                {
                    FullName = "Alba Sutton",
                    Address = "4567 Pine Avenue, Edison, WA 97202",
                });

            await this.client.SendEventAsync(@event);
        }
    }
}
