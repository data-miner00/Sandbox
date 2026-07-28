namespace Sandbox.Experiment;

using System;

internal class UseThreadLock
{
#pragma warning disable IDE0330 // Use 'System.Threading.Lock'
    private readonly object oldLock = new();
#pragma warning restore IDE0330 // Use 'System.Threading.Lock'
    private readonly Lock newLock = new(); // new lock

    private long oldLockCounter = 0;
    private long newLockCounter = 0;

    public static async Task DemoAsync()
    {
        var demo = new UseThreadLock();

        var oldLockTasks = Enumerable.Range(0, 100).Select(x => demo.ProcessUsingOldLockAsync());
        var newLockTasks = Enumerable.Range(0, 100).Select(x => demo.ProcessUsingNewLockAsync());

        await Task.WhenAll(oldLockTasks.Concat(newLockTasks));
    }

    public async Task ProcessUsingOldLockAsync()
    {
        lock (this.oldLock)
        {
            Console.WriteLine($"[{DateTime.Now.Ticks}] Old counter: {this.oldLockCounter++}");
        }
    }

    public async Task ProcessUsingNewLockAsync()
    {
        lock (this.newLock)
        {
            Console.WriteLine($"[{DateTime.Now.Ticks}] New counter: {this.newLockCounter++}");
        }
    }
}
