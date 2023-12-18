namespace Sandbox.Concepts.IO.JSON
{
    using System;

    public sealed class Book
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Genre { get; set; }

        public string ISBN { get; set; }

        public DateTime PublicationDate { get; set; }

        public IList<string> Tags { get; set; }
    }
}
