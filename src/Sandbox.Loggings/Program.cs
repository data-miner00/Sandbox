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

            logger.Information("Added user {UserName}, Age {UserAge}. Added on {Created}. Guid: {Guid}, {List}", "hello", 23, DateTime.Now, Guid.NewGuid(), new List<string> { "hello", "world" });

            // Destructure object properties
            // If no destructure, Serilog will call the ToString() method for the class
            logger.Information("Color is: {@Color}", new Color { Red = 255 });
        }

        public static void RollingLog()
        {
            // rollinglogfile-20140520.log
            var logger = new LoggerConfiguration()
                .WriteTo.File("rollinglogfile.log", retainedFileCountLimit: 2) // Only 2 concurrent log file can exist.
                .CreateLogger();

            logger.Information("Hello world");
        }

        public static void DestructureByTransform()
        {
            var logger = new LoggerConfiguration()
                .Destructure.ByTransforming<Color>(x => new { x.Red })
                .WriteTo.Console()
                .CreateLogger();

            logger.Information("Fave {Color}", new Color { Red = 255 });
        }

        private sealed class Color
        {
            public int Red { get; set; }

            public override string ToString()
            {
                return "Red" + this.Red.ToString();
            }
        }

        /*
         * Sinks are place we can put the data in.
         *
         * Console
         * File/RollingFile
         * Log4net
         * Loggly
         * Loggr
         * ElasticSearch
         * ElmahIO
         * ElasticSearch
         * MongoDB
         * RavenDB
         * Seq
         * TextWriter
         * CouchDB
         */
    }
}
