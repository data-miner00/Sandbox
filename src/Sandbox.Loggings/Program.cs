namespace Sandbox.Loggings
{
    using Serilog;

    public static class Program
    {
        public static void Main(string[] args)
        {
            const string logFormat = "{Timestamp:yyyy-MMM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";

            var logger = new LoggerConfiguration()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Verbose, outputTemplate: logFormat)
                .CreateLogger();

            logger.Information("Added user");

            // If file size is already maxed, no new logs will be added.
            var logger1 = new LoggerConfiguration()
                .WriteTo.File("info.log", Serilog.Events.LogEventLevel.Verbose, outputTemplate: logFormat, fileSizeLimitBytes: 100000)
                .CreateLogger();

            logger1.Information("hello world");
        }
    }
}
