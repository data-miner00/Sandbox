namespace Sandbox.Aws.Handlers;

using System.Threading.Tasks;
using MediatR;
using Sandbox.Aws.Events;
using Serilog;

/// <summary>
/// The handler for the <see cref="CustomerCreatedEvent"/>.
/// </summary>
public class CustomerCreatedHandler : IRequestHandler<CustomerCreatedEvent>
{
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerCreatedHandler"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> implementation.</param>
    public CustomerCreatedHandler(ILogger logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// The handling method for the event.
    /// </summary>
    /// <param name="event">The <see cref="CustomerCreatedEvent"/>.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Nothing.</returns>
    public Task Handle(CustomerCreatedEvent @event, CancellationToken cancellationToken)
    {
        this.logger.Information(@event.Name);
        return Task.CompletedTask;
    }
}
