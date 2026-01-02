using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Core
{
    /// <summary>
    /// Middleware that forwards the X-Forwarded-For header and appends the current connection's IP address
    /// </summary>
    public class XForwardedForMiddleware
    {
        private readonly RequestDelegate _next;

        public XForwardedForMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get the current connection's remote IP address
            var remoteIp = context.Connection.RemoteIpAddress?.ToString();

            if (!string.IsNullOrEmpty(remoteIp))
            {
                // Get existing X-Forwarded-For header value
                var existingForwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                // Build the new X-Forwarded-For value
                string newForwardedFor;
                if (!string.IsNullOrEmpty(existingForwardedFor))
                {
                    // Append current IP to existing chain
                    newForwardedFor = $"{existingForwardedFor}, {remoteIp}";
                }
                else
                {
                    // Start new chain with current IP
                    newForwardedFor = remoteIp;
                }

                // Set the updated X-Forwarded-For header
                context.Request.Headers["X-Forwarded-For"] = newForwardedFor;
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }

    /// <summary>
    /// Extension method to register the middleware
    /// </summary>
    public static class XForwardedForMiddlewareExtensions
    {
        public static IApplicationBuilder UseXForwardedFor(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<XForwardedForMiddleware>();
        }
    }
}
