namespace Sandbox.Aws.Handlers;

using System.Threading.Tasks;
using MediatR;
using Sandbox.Aws.Events;
using Serilog;

/// <summary>
/// The handler for the <see cref="CustomerDeletedEvent"/>.
/// </summary>
public class CustomerDeletedHandler : IRequestHandler<CustomerDeletedEvent>
{
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerDeletedHandler"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="Ilogger"/> implementation.</param>
    public CustomerDeletedHandler(ILogger logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// The handling method for the event.
    /// </summary>
    /// <param name="event">The <see cref="CustomerDeletedEvent"/>.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Nothing.</returns>
    public Task Handle(CustomerDeletedEvent @event, CancellationToken cancellationToken)
    {
        this.logger.Information(@event.Id.ToString());
        return Task.CompletedTask;
    }
}
