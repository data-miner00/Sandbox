namespace Sandbox.Silo
{
    using System;
    using System.Text;

    /// <summary>
    /// Working with <c>stackalloc</c>. From <see href="https://www.youtube.com/watch?v=YjnnWbwwAkc">Everything (maybe too much) about stackalloc</see>.
    /// </summary>
    public static class StackallocStrings
    {
        private static readonly char[] Charset = "0123456789abcdef".ToCharArray();
        private static readonly Random Random = new();

        public static string GetRandomString(int length)
        {
            Span<char> buffer = stackalloc char[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = Charset[Random.Next(Charset.Length)];
            }

            return buffer.ToString(); // new string(buffer);
        }

        internal static int NewApi(ReadOnlySpan<char> source, Span<char> destination)
        {
            return 0;
        }

        public static string GeneratePrefixedString(string prefix, int length)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

            if (checked(prefix.Length + length) > 64)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            Span<char> buffer = stackalloc char[prefix.Length + length];
            prefix.AsSpan().CopyTo(buffer);

            for (var i = prefix.Length; i < length; i++)
            {
                buffer[i] = Charset[Random.Next(Charset.Length)];
            }

            return buffer.ToString();
        }

        public static string GetUtf8Hex(string text)
        {
            Span<byte> buffer = stackalloc byte[256];
            int max = Encoding.UTF8.GetMaxByteCount(text.Length);

            if (max > buffer.Length)
            {
                // If max too big, use heap instead.
                buffer = new byte[max];
            }

            int written = Encoding.UTF8.GetBytes(text, buffer);
            return Convert.ToHexString(buffer[..written]);
        }
    }
}
