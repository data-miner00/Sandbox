using System.Reflection;
using Sandbox.WeatherApi.Filters;
using Swashbuckle.AspNetCore.Filters;
using dotenv.net;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

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
var a = builder.Configuration.GetSection("SHAUN").Value;
var c = builder.Configuration.GetSection("BEN").Value;

Console.WriteLine(b);
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
