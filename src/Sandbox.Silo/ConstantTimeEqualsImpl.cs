namespace Sandbox.Silo;

using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// This is to prevent timing attacks where the comparison leaks the correct length.
/// </summary>
public static class ConstantTimeEqualsImpl
{
    public static void Demo()
    {
        var string1 = Encoding.UTF8.GetBytes("hello");
        var string2 = Encoding.UTF8.GetBytes("bello");

        Console.WriteLine("Is 'hello' equal to 'hello'?");

        // Self implementation
        var isSame = ConstantTimeEquals(string1, string1);
        Console.WriteLine("Internal implementation: {0}", isSame ? "Yes" : "No");

        // Built-in
        var isSame2 = CryptographicOperations.FixedTimeEquals(string1, string1);
        Console.WriteLine("Built-in Cryptography: {0}", isSame2 ? "Yes" : "No");

        Console.WriteLine("------------------------------");
        Console.WriteLine("Is 'hello' equal to 'bello'?");

        // Self implementation
        var isSame3 = ConstantTimeEquals(string1, string2);
        Console.WriteLine("Internal implementation: {0}", isSame3 ? "Yes" : "No");

        // Built-in
        var isSame4 = CryptographicOperations.FixedTimeEquals(string1, string2);
        Console.WriteLine("Built-in Cryptography: {0}", isSame4 ? "Yes" : "No");
    }

    private static bool ConstantTimeEquals(byte[] left, byte[] right)
    {
        // probably not a good idea to do this
        if (left.Length != right.Length)
        {
            return false;
        }

        int diff = 0;

        for (int i = 0; i < left.Length; i++)
        {
            diff |= left[i] ^ right[i];
        }

        return diff == 0;
    }
}
