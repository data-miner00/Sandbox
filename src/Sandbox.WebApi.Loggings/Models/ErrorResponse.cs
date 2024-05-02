namespace Sandbox.WebApi.Loggings.Models;

public sealed record ErrorResponse
{
    public int StatusCode { get; init; }

    public string ReferenceId { get; init; }

    public string Message { get; init; }
}
