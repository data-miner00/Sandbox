namespace Sandbox.Silo;

using System;

public static class ConfigureAwaitTheads
{
    public static async Task ConundrumAsync()
    {
        Console.WriteLine("Thread ID: {0}", Environment.CurrentManagedThreadId);
        Console.WriteLine("\n\n");
        Console.WriteLine("Awaited with ConfigureAwait(true)");
        await Task.Run(() =>
        {
            Console.WriteLine("Inside first task Thread ID: {0}", Environment.CurrentManagedThreadId);
        }).ConfigureAwait(true);
        Console.WriteLine("Thread ID: {0}", Environment.CurrentManagedThreadId);
        Console.WriteLine("\n\n");
        Console.WriteLine("Awaited with ConfigureAwait(false)");
        await Task.Run(() =>
        {
            Console.WriteLine("Inside second task Thread ID: {0}", Environment.CurrentManagedThreadId);
        }).ConfigureAwait(false);
        Console.WriteLine("Thread ID: {0}", Environment.CurrentManagedThreadId);
        Console.WriteLine("\n\n");
        Console.WriteLine("Awaited NO ConfigureAwait");
        await Task.Run(() =>
        {
            Console.WriteLine("Inside third task Thread ID: {0}", Environment.CurrentManagedThreadId);
        });
        Console.WriteLine("Thread ID: {0}", Environment.CurrentManagedThreadId);
    }
}
