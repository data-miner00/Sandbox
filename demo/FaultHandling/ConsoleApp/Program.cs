using ConsoleApp;
using Serilog;

var logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var errorRate = 80;

var client = new DangerClient(errorRate, logger);

client.Execute();

logger.Information("Congratulations, the program ended in a success.");
