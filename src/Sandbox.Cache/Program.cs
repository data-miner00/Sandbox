namespace Sandbox.Cache;

using Microsoft.Extensions.Caching.Memory;

internal static class Program
{
    internal static void Main(string[] args)
    {
        var cacheOption = new MemoryCacheOptions
        {
            SizeLimit = 1024,
        };

        var cache = new MemoryCache(cacheOption);
        var ids = new HashSet<string>();

        for (var i = 0; i < 20; ++i)
        {
            var randomInt = GetRandomOneToThree();

            var cachedObject = cache.GetOrCreate(
                randomInt,
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    entry.SlidingExpiration = TimeSpan.FromSeconds(5);
                    entry.Size = 2;
                    entry.RegisterPostEvictionCallback((key, value, reason, state) =>
                    {
                        if (value is DisposableObject disposable)
                        {
                            ids.RemoveWhere(x => x == disposable.Name);
                            disposable.Dispose();

                            Console.WriteLine($"Current cache count: {cache.Count}");
                            Console.WriteLine($"Current cache names: [{string.Join(',', ids.Order())}]");
                        }
                    });

                    var id = Guid.NewGuid().ToString();
                    ids.Add(id);

                    return new DisposableObject(id);
                });

            Console.WriteLine($"Current cache count: {cache.Count}");
            Console.WriteLine($"Current cache names: [{string.Join(',', ids.Order())}]");

            cachedObject?.Execute();

            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }

    internal static int GetRandomOneToThree()
    {
        var random = new Random();
        return random.Next(1, 4);
    }
}
