#if NET481
using System;
#endif

namespace Sandbox.Nuget.MultiPlatform
{
    public class Greetings
    {
        private static string FriendlyTime
        {
            get
            {
                var now = DateTime.Now;

                return now.ToShortDateString();
            }
        }

        public static string Hello
        {
            get
            {
#if NET481
                return $"Hello, World from .NET Framework 4.8.1 {FriendlyTime}";
#elif NET8_0
                return $"Hello, World from .NET 8.0 {FriendlyTime}";
#elif NET10_0
                return $"Hello, World from .NET 10.0 {FriendlyTime}";
#else
#error Platform not supported.
#endif
            }
        }
    }
}
