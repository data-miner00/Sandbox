namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Text;

    public static class Encodings
    {
        public static void AsciiEncodingExample(string original)
        {
            var ascii = new ASCIIEncoding();
            var asciiEncodedBytes = ascii.GetBytes(original);

            foreach (Byte b in asciiEncodedBytes)
            {
                Console.Write($"{b},");
            }
        }

        public static void UnicodeEncodingExample(string original)
        {
            var unicode = new UnicodeEncoding();
            var unicodeEncodedBytes = unicode.GetBytes(original);

            foreach (Byte b in unicodeEncodedBytes)
            {
                Console.Write($"{b},");
            }
        }

        public static void UTF32EncodingExample(string original)
        {
            var utf32Encoding = new UTF32Encoding();
            var utf32EncodedBytes = utf32Encoding.GetBytes(original);

            foreach (Byte b in utf32EncodedBytes)
            {
                Console.Write($"{b},");
            }
        }

        public static void UTF8EncodingExample(string original)
        {
            var utf8Encoding = new UTF8Encoding();
            var utf8EncodedBytes = utf8Encoding.GetBytes(original);

            foreach (Byte b in utf8EncodedBytes)
            {
                Console.Write($"{b},");
            }
        }

        public static void UTF8DecodingExample(byte[] utf8EncodedBytes)
        {
            var utf8Decoder = Encoding.UTF8.GetDecoder();

            // Get how many chars composed the string array
            var charCount = utf8Decoder.GetCharCount(utf8EncodedBytes, 0, utf8EncodedBytes.Length);
            var chars = new char[charCount];

            // Decoding byte array
            var charsDecodedCount = utf8Decoder.GetChars(utf8EncodedBytes, 0, utf8EncodedBytes.Length, chars, 0);

            // Decoded chars
            foreach (var c in chars)
            {
                Console.Write($"{c} ");
            }
        }

        public static void UnsupportedCharacters()
        {
            var original = "Hello World 世界は怖い 한국어 연습해주세용 Γειά σου, Κόσμε!";

            Console.WriteLine(Console.OutputEncoding.ToString());
            Console.WriteLine($"default (UTF8): {original}");

            Console.OutputEncoding = Encoding.ASCII;
            Console.WriteLine($"ASCII: {original}");

            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine($"Unicode: {original}");

            Console.OutputEncoding = Encoding.UTF32;
            Console.WriteLine($"UTF32: {original}");
        }
    }
}
