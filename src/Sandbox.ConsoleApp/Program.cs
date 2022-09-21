namespace Sandbox.ConsoleApp
{
    using Sandbox.FSharp.Library;
    using Sandbox.SQLite;
    using Sandbox.VisualBasic.Library;

    internal class Program
    {
        static void Main(string[] args)
        {
            LoadEnumArticle();
        }

        private static void Test()
        {
            Say.hello("Me");

            var person = new Person
            {
                FirstName = "first",
                LastName = "last",
            };

            var user = new User
            {
                Username = string.Empty,
                Password = string.Empty,
                Favorites = new List<string> { "item1", "item2", "item3" },
            };

            LoadPeopleList();

            for (var i = 0; i < 2; i++)
            {
                var firstName = Console.ReadLine();
                var lastName = Console.ReadLine();

                SavePerson(firstName!, lastName!);
            }
        }

        private static void LoadPeopleList()
        {
            var people = SQLiteDataAccess.LoadPeople();
            int i = 1;

            foreach (var person in people)
            {
                Console.WriteLine($"{i++}. FN: {person.FirstName}, LN: {person.LastName}");
            }
        }

        private static void SavePerson(string firstName, string lastName)
        {
            var person = new Person
            {
                FirstName = firstName,
                LastName = lastName,
            };

            SQLiteDataAccess.SavePerson(person);
        }

        private static void LoadComplexUser()
        {
            var user = new User
            {
                Username = "my name",
                Password = "123",
                Favorites = new List<string>
                {
                    "fav_1",
                    "fav_2",
                    "fav_3",
                },
            };

            TryComplexDataStructure.SaveUser(user);

            var users = TryComplexDataStructure.LoadUser();

            Console.ReadLine();
        }

        private static void LoadEnumArticle()
        {
            var article = new Article
            {
                Category = Category.Science,
                Title = "Title",
                Id = 1,
            };

            TryComplexDataStructureWithEnum.SaveArticle(article);

            var articles = TryComplexDataStructureWithEnum.LoadArticles();

            Console.ReadLine();
        }
    }
}
