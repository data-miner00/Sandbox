namespace Sandbox.Concepts.IO.XML
{
    using System.Xml;

    public static class XmlExamples
    {
        public static void Examples()
        {
            var doc = new XmlDocument();
            doc.Load("books.xml");

            Console.WriteLine(doc.InnerXml);

            var rootNodes = doc.ChildNodes;

            // printing the name of node name
            foreach (XmlNode node in rootNodes)
            {
                Console.WriteLine(node.Name);
            }

            // Get books node
            var books = doc.ChildNodes[1];
            Console.WriteLine(books.InnerXml);

            // Get first book
            var firstBook = books.FirstChild;
            Console.WriteLine(firstBook.InnerText);

            // Get genre
            var genre = firstBook.Attributes["genre"];
            Console.WriteLine(genre.Value);

            // Edit an existing node
            firstBook.ChildNodes[1].InnerText = "14.99";

            // Add a new node
            var newBook = doc.CreateElement("book");
            var genreAttr = doc.CreateAttribute("genre");
            genreAttr.Value = "Science fiction";
            newBook.Attributes.Append(genreAttr);

            var isbnAttr = doc.CreateAttribute("ISBN");
            isbnAttr.Value = "3-103900-09-0";
            newBook.Attributes.Append(isbnAttr);

            var publicationDateAttr = doc.CreateAttribute("publicationdate");
            publicationDateAttr.Value = "19.25";
            newBook.Attributes.Append(publicationDateAttr);

            var newBookTitle = doc.CreateElement("title");
            newBookTitle.InnerText = "Title";

            var newBookPrice = doc.CreateElement("price");
            newBookPrice.InnerText = "19.25";

            newBook.AppendChild(newBookTitle);
            newBook.AppendChild(newBookPrice);

            books.AppendChild(newBook);

            // Delete a node
            books.RemoveChild(books.FirstChild);
        }

        public static void QueryXml()
        {
            var bookstore = new XmlDocument();
            bookstore.Load("bookstore.xml");

            // query all the element with path
            var books = bookstore.SelectNodes("bookstore/book");

            foreach (XmlNode book in books)
            {
                Console.WriteLine(book.Attributes["ISBN"].Value);
            }

            // query all titles
            var titles = bookstore.SelectNodes("bookstore/book/title");

            foreach (XmlNode title in titles)
            {
                Console.WriteLine(title.InnerText);
            }

            // filtering with attribute
            // get all books where genre is philosophy
            var isbnBooks = bookstore.SelectNodes("bookstore/book[@genre='philosophy']");

            foreach (XmlNode book in isbnBooks)
            {
                Console.WriteLine(book.ChildNodes[0].InnerText);
            }
        }

        public static void AddNamespaceToExistingXml()
        {
            var doc = new XmlDocument();
            doc.Load("bookstore-namespaced.xml");
            var root = doc.DocumentElement;

            // Add namespace
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "contoso:books-schema");

            var node = root.SelectSingleNode("descendant::bk:book[bk:author/bk:kast-name='Kingsolver']", nsmgr);
            Console.WriteLine(node.InnerXml);
        }
    }
}
