namespace Sandbox.Concepts.IO.XML
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    public sealed class Book
    {
        /// <summary>
        /// Setting isNullable to false tells the serializer that
        /// the XML will no appear if Title is set to null.
        /// </summary>
        [XmlElement(IsNullable = false)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        [XmlAttribute]
        public string Genre { get; set; }

        [XmlAttribute]
        public string ISBN { get; set; }

        [XmlAttribute("publicationdate")]
        public DateTime PublicationDate { get; set; }
    }
}
