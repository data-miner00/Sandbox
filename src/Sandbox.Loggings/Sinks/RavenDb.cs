namespace Sandbox.Loggings.Sinks
{
    using Raven.Client.Documents;
    using Serilog;

    internal class RavenDb
    {
        public void Initialize()
        {
            var logStore = new DocumentStore
            {
                Urls = new[] { "http:localhost:8080" },
                Database = "logs",
            };

            logStore.Initialize();

            var logger = new LoggerConfiguration()
                .WriteTo.RavenDB(logStore)
                .CreateLogger();
        }

        private sealed class User
        {
            public int Id { get; set; }

            public string? Name { get; set; }
        }
    }
}
