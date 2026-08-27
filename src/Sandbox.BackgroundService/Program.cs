using Sandbox.BackgroundService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MyService>();

var app = builder.Build();
app.MapGet("/", () => "Application is running background tasks!");
app.Run();
