namespace Sandbox.Aws.Handlers;

using System.Threading.Tasks;
using MediatR;
using Sandbox.Aws.Events;
using Serilog;

/// <summary>
/// The handler for the <see cref="CustomerUpdatedEvent"/>.
/// </summary>
public class CustomerUpdatedHandler : IRequestHandler<CustomerUpdatedEvent>
{
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerUpdatedHandler"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> implementation.</param>
    public CustomerUpdatedHandler(ILogger logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// The handling method for the event.
    /// </summary>
    /// <param name="event">The <see cref="CustomerUpdatedEvent"/>.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Nothing.</returns>
    public Task Handle(CustomerUpdatedEvent @event, CancellationToken cancellationToken)
    {
        this.logger.Information(@event.Name);
        return Task.CompletedTask;
    }
}
