namespace Sandbox.Core.Events
{
    using System;

    public class CustomerUpdatedEvent : ISqsMessage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
