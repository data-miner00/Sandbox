using Microsoft.FeatureManagement.FeatureFilters;

namespace WeatherApi
{
    public class FeatureTargetingContext : ITargetingContextAccessor
    {
        private const string UserIdHeader = "UserId";
        private const string UserGroupIdHeader = "UserGroupId";
        private readonly IHttpContextAccessor context;
        private readonly ILogger<FeatureTargetingContext> logger;

        public string UserId
        {
            get
            {
                return context.HttpContext?.Request.Headers.TryGetValue(UserIdHeader, out var userId) == true
                    ? userId.ToString()
                    : string.Empty;
            }
        }

        public IEnumerable<string> Groups
        {
            get
            {
                if (context.HttpContext?.Request.Headers.TryGetValue(UserGroupIdHeader, out var userGroupId) == true)
                {
                    var raw = userGroupId.FirstOrDefault();
                    return string.IsNullOrEmpty(raw)
                        ? []
                        : raw.Split(',');
                }

                return [];
            }
        }

        public FeatureTargetingContext(IHttpContextAccessor context, ILogger<FeatureTargetingContext> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public ValueTask<TargetingContext> GetContextAsync()
        {
            if (context.HttpContext == null)
            {
                return new ValueTask<TargetingContext>(new TargetingContext());
            }

            var targetingContext = new TargetingContext
            {
                UserId = this.UserId,
                Groups = this.Groups,
            };

            this.logger.LogInformation("UserId: {UserId}", targetingContext.UserId);

            return new ValueTask<TargetingContext>(targetingContext);
        }
    }
}
