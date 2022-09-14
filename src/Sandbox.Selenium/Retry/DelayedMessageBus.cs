namespace Sandbox.Selenium.Retry
{
    using Xunit.Abstractions;
    using Xunit.Sdk;

    internal class DelayedMessageBus : IMessageBus
    {
        private readonly IMessageBus innerBus;
        private readonly List<IMessageSinkMessage> messages = new List<IMessageSinkMessage>();

        public DelayedMessageBus(IMessageBus innerBus)
        {
            this.innerBus = innerBus;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool QueueMessage(IMessageSinkMessage message)
        {
            lock (this.messages)
            {
                messages.Add(message);
            }
            return true;
        }
    }
}
