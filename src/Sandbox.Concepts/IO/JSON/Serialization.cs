﻿namespace Sandbox.Concepts.IO.JSON
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Threading.Tasks;

    public static class Serialization
    {
        public static void SerializeToJson()
        {
            var book = new Book
            {
                Title = "Times Magazine",
                Price = 133.99m,
                PublicationDate = DateTime.Now,
                Genre = "Magazine",
                ISBN = "Autogenerated ISBN",
                Tags = new List<string> { "magazine", "fashion" },
            };

            var jsonString = JsonSerializer.Serialize(book);
        }

        public static async Task SerializeToJsonFile(string filename)
        {
            using var fs = File.Create(filename);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var book = new Book
            {
                Title = "Times Magazine",
                Price = 133.99m,
                PublicationDate = DateTime.Now,
                Genre = "Magazine",
                ISBN = "Autogenerated ISBN",
                Tags = new List<string> { "magazine", "fashion" },
            };

            await JsonSerializer.SerializeAsync(fs, book, options);
        }

        public static void DeserializeFromJson(string filename)
        {
            var jsonFile = File.ReadAllText(filename);

            var book = JsonSerializer.Deserialize<Book>(jsonFile);

            Console.WriteLine($"Publish Date: {book?.PublicationDate}");
        }

        public static void DeserializeFromJsonWithJsonDocument(string filename)
        {
            var jsonString = File.ReadAllText(filename);

            using var document = JsonDocument.Parse(jsonString);
            var root = document.RootElement;

            // Get entire JSON from root
            Console.WriteLine(root.ToString());

            var tags = root.GetProperty("tags");
            Console.WriteLine(tags.ToString());

            // Iterate over JsonElement
            foreach (var tag in tags.EnumerateArray())
            {
                Console.WriteLine(tag.ToString());

                // tag.GetInt32();
                // tag.GetDouble();
            }
        }

        public static void UsingJsonNode(string originalFile, string extraFile)
        {
            var jsonOriginal = File.ReadAllText(originalFile);
            var jsonExtra = File.ReadAllText(extraFile);

            var originalNode = JsonNode.Parse(jsonOriginal);

            // Cast as JsonObject to get number of elements
            Console.WriteLine(originalNode.AsObject().Count());

            // Get list of all keys included in object
            var keys = originalNode.AsObject().Select(child => child.Key).ToList();
            Console.WriteLine(string.Join(", ", keys));

            // Get value & type of one JsonNode
            var title = originalNode["title"];
            Console.WriteLine($"Type = {title.GetType()}");
            Console.WriteLine($"Json = {title.ToJsonString()}");

            // Get array
            var tags = originalNode["tags"];
            Console.WriteLine($"Type = {tags.GetType()}");
            Console.WriteLine($"Json = {tags.ToJsonString()}");

            Console.WriteLine($"Type = {tags[0].GetType()}");
            Console.WriteLine($"Json = {tags[0].ToJsonString()}");

            // Properties can be chained
            // originalNode["first"]["nested"]

            // Remove node
            originalNode.AsObject().Remove("title");

            // Add new property
            originalNode.AsObject().Add("new key", "new value");

            // Append node
            var extraNode = JsonNode.Parse(jsonExtra);
            originalNode.AsObject().Add("extra", extraNode);
        }

        public static void CreateWithJsonObject()
        {
            var bookObject = new JsonObject
            {
                ["title"] = "Cool magazine",
                ["price"] = 123m,
                ["tags"] = new JsonArray("hello", "world"),
                ["publicationDate"] = new DateTime(2020, 01, 01, 00, 00, 00, DateTimeKind.Utc),
                ["isbn"] = "Autogenerated ISBN",
                ["genre"] = "horror",
            };
        }
    }
}
