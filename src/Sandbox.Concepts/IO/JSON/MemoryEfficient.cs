namespace Sandbox.Concepts.IO.JSON
{
    using System;
    using System.Text;
    using System.Text.Json;

    public static class MemoryEfficient
    {
        /// <summary>
        /// Provides a high-performance API for forward-only, readonly
        /// access to UTF-8 encoded JSON.
        /// </summary>
        public static void HighPerformanceRead(string filename)
        {
            // BOM (byte order mark) which used to determine if it is UTF8
            var utf8Bom = new byte[] { 0xEF, 0xBB, 0xBF };

            ReadOnlySpan<byte> jsonReadOnlySpan = File.ReadAllBytes(filename);

            // Read past the UTF8 BOM bytes if a BOM exist
            if (jsonReadOnlySpan.StartsWith(utf8Bom))
            {
                jsonReadOnlySpan = jsonReadOnlySpan[utf8Bom.Length..];
            }

            var reader = new Utf8JsonReader(jsonReadOnlySpan);

            var text = string.Empty;

            while (reader.Read())
            {
                var tokenType = reader.TokenType;
                Console.WriteLine(tokenType.ToString());

                switch (tokenType)
                {
                    case JsonTokenType.StartObject:
                        break;
                    case JsonTokenType.PropertyName:
                    case JsonTokenType.String:
                        text = reader.GetString();
                        Console.WriteLine(" " + text);
                        break;
                    case JsonTokenType.Number:
                        Console.WriteLine(reader.GetInt64());
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Provides a high performance API for forward only, non-cached
        /// writing of UTF-8 encoded JSON.
        /// </summary>
        public static void HighPerformanceWrite(string outputFilename)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });

            writer.WriteStartObject();

            // Add property
            writer.WritePropertyName("title");
            writer.WriteStringValue("Magazine Vogue");

            // Add property same effect
            writer.WriteString("publicationDate", DateTimeOffset.UtcNow);

            // Add number property
            writer.WriteNumber("rating", 5.00);

            // Add nested object
            writer.WriteStartObject("nested");

            writer.WriteNumber("number1", 123);
            writer.WriteNumber("number2", 223);

            writer.WriteEndObject();

            // Add array
            writer.WriteStartArray("tags");

            writer.WriteStringValue("hello");
            writer.WriteNullValue();
            writer.WriteStringValue("world");

            writer.WriteEndArray();

            // Add null
            writer.WriteNull("dummy");

            writer.WriteEndObject();
            writer.Flush();

            var json = Encoding.UTF8.GetString(stream.ToArray());

            File.WriteAllText(outputFilename, json);
        }
    }
}
