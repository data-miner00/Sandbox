namespace Client.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
