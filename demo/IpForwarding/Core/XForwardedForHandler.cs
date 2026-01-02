using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    /// <summary>
    /// Delegating handler that adds X-Forwarded-For header to outbound requests
    /// </summary>
    public class XForwardedForHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public XForwardedForHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            
            if (httpContext?.Items.ContainsKey(Constants.XForwardedForKey) == true)
            {
                var forwardedFor = httpContext.Items[Constants.XForwardedForKey] as string;
                
                if (!string.IsNullOrEmpty(forwardedFor))
                {
                    // Add or update X-Forwarded-For header in outbound request
                    if (request.Headers.Contains("X-Forwarded-For"))
                    {
                        request.Headers.Remove("X-Forwarded-For");
                    }
                    request.Headers.Add("X-Forwarded-For", forwardedFor);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

    /// <summary>
    /// Extension method to register the handler
    /// </summary>
    public static class XForwardedForHandlerExtensions
    {
        public static IHttpClientBuilder AddXForwardedForHandler(this IHttpClientBuilder builder)
        {
            return builder.AddHttpMessageHandler<XForwardedForHandler>();
        }
    }
}
