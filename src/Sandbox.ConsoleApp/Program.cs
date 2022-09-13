namespace Sandbox.ConsoleApp
{
    using Sandbox.FSharp.Library;
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
        }
    }
}