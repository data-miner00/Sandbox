namespace Sandbox.WeatherApi;

using System.Reflection;
using dotenv.net;
using Sandbox.WeatherApi.Filters;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

public static class Program
{
    public static void Main(string[] args)
    {
        DotEnv.Load();

        var builder = WebApplication.CreateBuilder(args).ConfigureServices();

        var app = builder.Build();

        app.UseSerilogRequestLogging();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    private static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(config =>
        {
            // The very first filter that a request reach
            config.Filters.Add(new GlobalFilter());
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.ExampleFilters();
        });

        var b = builder.Configuration.GetSection("CABAL_DIR").Value;

        Console.WriteLine(b);
        builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });

        return builder;
    }
}
