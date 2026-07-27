namespace Sandbox.Cache;

using System;

internal class DisposableObject : IDisposable
{
    private readonly string name;
    private readonly object gate = new();

    private bool isDisposed;
    private int count = 0;

    public DisposableObject(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.name = name;
    }

    public int Count => this.count;

    public void Execute()
    {
        lock (this.gate)
        {
            this.count++;
        }
    }

    public void Dispose()
    {
        Console.WriteLine("Dispose method called. " + this.name);

        if (!this.isDisposed)
        {
            this.isDisposed = true;

            Console.WriteLine($"{this.name} object disposed.");
        }
    }
}
