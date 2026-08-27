namespace Sandbox.BackgroundService;

using HostedService = Microsoft.Extensions.Hosting.BackgroundService;

public class MyService : HostedService
{
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Stopping...");
        await base.StopAsync(stoppingToken);
        Console.WriteLine("Stopped.");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await HeavyStuffs();
            await Task.Delay(1000);
        }

        Console.WriteLine("Executed Async");
    }

    private async Task HeavyStuffs()
    {
        Console.WriteLine("Hello World");

        await Task.Delay(10000);

        Console.WriteLine("Bye World");
    }
}
