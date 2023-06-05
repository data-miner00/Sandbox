namespace Sandbox.Aws.Handlers
{
    using System.Threading.Tasks;
    using MediatR;
    using Sandbox.Core.Events;
    using Serilog;

    public class CustomerDeletedHandler : IRequestHandler<CustomerDeletedEvent>
    {
        private readonly ILogger logger;

        public CustomerDeletedHandler(ILogger logger)
        {
            this.logger = logger;
        }

        public Task Handle(CustomerDeletedEvent @event, CancellationToken cancellationToken)
        {
            this.logger.Information(@event.Id.ToString());
            return Task.CompletedTask;
        }
    }
}
