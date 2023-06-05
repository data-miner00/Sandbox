namespace Sandbox.Core.Events
{
    using System;

    public class CustomerDeletedEvent : ISqsMessage
    {
        public Guid Id { get; set; }
    }
}
