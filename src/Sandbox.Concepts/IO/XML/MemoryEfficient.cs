namespace Sandbox.Concepts.IO.XML;

using System;
using System.Xml;

public static class MemoryEfficient
{
    /// <summary>
    /// It provides a fast, non-cached, forward-only way to read XML files.
    /// </summary>
    public static void UsingXmlReader()
    {
        using var fileStream = File.OpenText("books.xml");
        using var reader = XmlReader.Create(fileStream);

        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    Console.WriteLine($"Start element: {reader.Name}. Has Attributes? {reader.HasAttributes}");
                    break;
                case XmlNodeType.Text:
                    Console.WriteLine($"Inner text: {reader.Value}");
                    break;
                case XmlNodeType.CDATA:
                    Console.WriteLine("This is a CDATA");
                    break;
                case XmlNodeType.Whitespace:
                    Console.WriteLine("This is a whitespace");
                    break;
                case XmlNodeType.SignificantWhitespace:
                    Console.WriteLine("This is a significant whitespace");
                    break;
                case XmlNodeType.EndElement:
                    Console.WriteLine($"End element: {reader.Name}");
                    break;
                default:
                    Console.WriteLine($"Other element type: {reader.NodeType}");
                    break;
            }
        }
    }

    /// <summary>
    /// It provides a fast, non-cached, forward-only way to generate XML files.
    /// </summary>
    public static void UsingXmlWriter(string filename)
    {
        using var writer = XmlWriter.Create(filename);

        // <catalog>
        writer.WriteStartElement("catalog");

        // <book>
        writer.WriteStartElement("book");
        writer.WriteAttributeString(null, "id", null, "bk101");

        // <author>
        writer.WriteStartElement("author");
        writer.WriteString("Susan Cheong");

        // </author>
        writer.WriteEndElement();

        // <title>
        writer.WriteStartElement("title");
        writer.WriteString("Good reads");

        // </title>
        writer.WriteEndElement();

        // <publish_date>
        writer.WriteStartElement("publish_date");
        writer.WriteValue(new DateTime(2000, 01, 01, 00, 00, 00, DateTimeKind.Utc));

        // </publish_date>
        writer.WriteEndElement();

        // </book>
        writer.WriteEndElement();

        // </catalog>
        writer.WriteEndElement();

        // Create the file
        writer.Flush();
    }
}
