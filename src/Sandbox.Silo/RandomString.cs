namespace Sandbox.Silo
{
    using System;
    using System.Linq;

    /// <summary>
    /// Generate Random strings.
    /// Adapted from <see href="https://stackoverflow.com/questions/1344221/how-can-i-generate-Random-alphanumeric-strings">Stack Overflow</see>.
    /// </summary>
    internal static class RandomString
    {
        private static readonly Random Random = new();

        public static string Generate1(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string Generate2(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}
