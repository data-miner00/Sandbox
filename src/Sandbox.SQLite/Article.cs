namespace Sandbox.SQLite
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }
    }

    public enum Category
    {
        Technology,
        Health,
        Lifestyle,
        Education,
        Entertainment
    }
}
