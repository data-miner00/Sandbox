namespace Sandbox.Cache
{
    using Microsoft.Extensions.Caching.Memory;

    internal class Program
    {
        static void Main(string[] args)
        {
            var cacheOption = new MemoryCacheOptions
            {
                SizeLimit = 1024,
            };

            var cache = new MemoryCache(cacheOption);

            Console.WriteLine($"Current cache count: {cache.Count}");

            for (var i = 0; i < 20; ++i)
            {
                var randomInt = GetRandomOneToThree();

                var @object = cache.GetOrCreate(
                    randomInt,
                    entry =>
                    {
                        entry.SlidingExpiration = TimeSpan.FromSeconds(5);
                        entry.Size = 2;
                        entry.RegisterPostEvictionCallback((key, value, reason, state) =>
                        {
                            if (value is IDisposable disposable)
                            {
                                disposable.Dispose();
                            }
                        });

                        return new DisposableObject();
                    });

                Console.WriteLine($"Current cache count: {cache.Count}");

                @object.Execute();

                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        }

        static int GetRandomOneToThree()
        {
            var random = new Random();
            return random.Next(1, 4);
        }
    }
}
