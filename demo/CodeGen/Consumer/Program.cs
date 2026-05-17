namespace Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var user = new User
            {
                Id = 1,
                Name = "Shaun Chong",
                Email = "shaun@email.com",
            };

            Console.WriteLine(user);
        }
    }
}
