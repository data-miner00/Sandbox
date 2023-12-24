#define B
#define A
#undef B // undefining B

#define VERBOSE

#pragma warning disable S1186 // disable warning

namespace Sandbox.Concepts.Bcl;

using System;
using System.Diagnostics;

public static class Preprocessor
{
    [Conditional("DEBUG")] // conditionally include code when compiling
    public static void ReleaseMode()
    {
#if DEBUG
        Console.WriteLine("Debug version");
#elif RELEASE
        Console.WriteLine("Release version");
#endif
    }

    [Conditional("VERBOSE")]
    public static void Platform()
    {
#if NET8_0
        Console.WriteLine("It's .NET 8");
#elif NET48
        Console.WriteLine("It's .NET Framework 4.8");
#elif NETSTANDARD2_1
        Console.WriteLine("It's .NET Standard 2.1");
#endif
    }

    public static void Empty()
    {
    }
}
