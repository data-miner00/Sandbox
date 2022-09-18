namespace Sandbox.ConsoleApp
{
    using Sandbox.FSharp.Library;
    using Sandbox.SQLite;
    using Sandbox.VisualBasic.Library;

    internal class Program
    {
        static void Main(string[] args)
        {
            Say.hello("Me");

            var person = new Person
            {
                FirstName = "first",
                LastName = "last",
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
    }
}