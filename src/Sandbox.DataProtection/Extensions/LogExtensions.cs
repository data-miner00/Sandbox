namespace Sandbox.DataProtection.Extensions
{
    public static partial class LogExtensions
    {
        [LoggerMessage(LogLevel.Information, "User obtained")]
        public static partial void LogUserObtained<T>(
            this ILogger<T> logger,
            [LogProperties] User user);
    }
}
