namespace Sandbox.Aws.Handlers
{
    using System.Threading.Tasks;
    using Sandbox.Core.Events;
    using Serilog;

    public class CustomerDeletedHandler
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
