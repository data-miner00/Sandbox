namespace Sandbox.Silo;

using Microsoft.Extensions.Time.Testing;
using System;
using System.Collections.Generic;

public static class UseTimeProvider
{
    public static void Demo()
    {
        HashSet<TimeProvider> timeProviders =
        [
            SystemProvider,
            FakeProvider,
            CustomProvider,
        ];

        foreach (var provider in timeProviders)
        {
            TimeProviderConsumer(provider);
        }
    }

    private static TimeProvider SystemProvider => TimeProvider.System;

    private static TimeProvider FakeProvider
    {
        get
        {
            var provider = new FakeTimeProvider();
            provider.SetUtcNow(new DateTimeOffset(2067, 1, 1, 0, 0, 0, TimeSpan.Zero));
            return provider;
        }
    }

    private static TimeProvider CustomProvider => new CustomTimeProvider();

    private static void TimeProviderConsumer(TimeProvider timeProvider)
    {
        Console.WriteLine($"Now is {timeProvider.GetUtcNow()}");
    }
}

file class CustomTimeProvider : TimeProvider
{
    public override DateTimeOffset GetUtcNow()
    {
        return new DateTimeOffset(2067, 1, 1, 0, 0, 0, TimeSpan.Zero);
    }
}
