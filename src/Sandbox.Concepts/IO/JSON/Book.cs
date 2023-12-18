namespace Sandbox.Concepts.IO.JSON
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A sample book implementation with JsonPropertyAttribute.
    /// Additionally implements the IEquatable interface and implements operator.
    /// </summary>
    public class Book : IEquatable<Book>
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("genre")]
        public string Genre { get; set; }

        [JsonPropertyName("isbn")]
        public string ISBN { get; set; }

        [JsonPropertyName("publication_date")]
        public DateTime PublicationDate { get; set; }

        [JsonPropertyName("tags")]
        public IList<string> Tags { get; set; }

        #region Other stuffs
        public static bool operator !=(Book left, Book right)
        {
            return !(left == right);
        }

        public static bool operator ==(Book left, Book right)
        {
            if (left != right)
            {
                if (left is not null)
                {
                    return left.Equals(right);
                }

                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return int.MaxValue;
        }

        public virtual bool Equals(Book? other)
        {
            // very crude equavalency
            return this.Title == other?.Title;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Book);
        }
        #endregion
    }
}
