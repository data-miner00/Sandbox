namespace Sandbox.Experiment;

using System;

internal static class LongOverflow
{
    public static void Demo()
    {
        LongDemo();
        UlongDemo();
    }

    public static void LongDemo()
    {
        Console.WriteLine($"The long value: {long.MaxValue}"); // 9223372036854775807
        Console.WriteLine("It is a very big number. About nine quintillion two hundred and twenty-three quadrillion.");

        Console.WriteLine("Compile time overflow is forbidden by default. Doing 'long.MaxValue + 1' will not compile.");
        // var d = long.MaxValue + 1;

        Console.WriteLine("Use `unchecked` scope to explicitly allow overflow at compile time.");

        unchecked
        {
            Console.WriteLine($"long.MaxValue + 1 = {long.MaxValue + 1}"); // -9223372036854775808
        }

        Console.Write("Runtime overflow. Enter the number '1': ");
        var number = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine($"long.MaxValue + {number} = {long.MaxValue + number}");
    }

    public static void UlongDemo()
    {
        Console.WriteLine($"The ulong value: {ulong.MaxValue}"); // 18446744073709551615
        Console.WriteLine("It is a very big number. About eighteen quintillion four hundred and forty-six quadrillion.");

        Console.WriteLine("Compile time overflow is forbidden by default. Doing 'ulong.MaxValue + 1' will not compile.");
        // var d = ulong.MaxValue + 1;

        Console.WriteLine("Use `unchecked` scope to explicitly allow overflow at compile time.");

        unchecked
        {
            Console.WriteLine($"ulong.MaxValue + 1 = {ulong.MaxValue + 1}"); // 0
        }

        Console.Write("Runtime overflow. Enter the number '1': ");
        var number = Convert.ToUInt64(Console.ReadLine());
        Console.WriteLine($"ulong.MaxValue + {number} = {ulong.MaxValue + number}");
    }
}
