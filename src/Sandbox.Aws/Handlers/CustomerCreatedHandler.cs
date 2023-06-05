namespace Sandbox.Aws.Handlers
{
    using System.Threading.Tasks;
    using MediatR;
    using Sandbox.Core.Events;
    using Serilog;

    public class CustomerCreatedHandler : IRequestHandler<CustomerCreatedEvent>
    {
        private readonly ILogger logger;

        public CustomerCreatedHandler(ILogger logger)
        {
            this.logger = logger;
        }

        public Task Handle(CustomerCreatedEvent @event, CancellationToken cancellationToken)
        {
            this.logger.Information(@event.Name);
            return Task.CompletedTask;
        }
    }
}
