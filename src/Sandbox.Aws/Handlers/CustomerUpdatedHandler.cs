namespace Sandbox.Aws.Handlers
{
    using System.Threading.Tasks;
    using Sandbox.Core.Events;
    using Serilog;

    public class CustomerUpdatedHandler
    {
        private readonly ILogger logger;

        public CustomerUpdatedHandler(ILogger logger)
        {
            this.logger = logger;
        }

        public Task Handle(CustomerUpdatedEvent @event, CancellationToken cancellationToken)
        {
            this.logger.Information(@event.Name);
            return Task.CompletedTask;
        }
    }
}
