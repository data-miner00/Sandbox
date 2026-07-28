namespace Sandbox.Experiment;

using System.Threading.Tasks;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await UseThreadLock.DemoAsync();
        Console.WriteLine();
    }
}
