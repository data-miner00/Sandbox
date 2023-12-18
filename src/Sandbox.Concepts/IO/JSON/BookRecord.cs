namespace Sandbox.Concepts.IO.JSON;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Book implemented as a record with Json serializable fields.
/// </summary>
/// <param name="Title">The title of the book.</param>
/// <param name="Price">The price of the book.</param>
/// <param name="Genre">The genre of the book.</param>
/// <param name="ISBN">The ISBN code of the book.</param>
/// <param name="PublicationDate">The publication date.</param>
/// <param name="Tags">The tags associated with it.</param>
public record BookRecord(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("price")] decimal Price,
    [property: JsonPropertyName("genre")] string Genre,
    [property: JsonPropertyName("isbn")] string ISBN,
    [property: JsonPropertyName("publication_date")] DateTime PublicationDate,
    [property: JsonPropertyName("tags")] IList<string> Tags);
