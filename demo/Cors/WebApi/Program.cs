using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var corsOptions = builder.Configuration.GetSection("Cors").Get<CorsOptions>();

ArgumentNullException.ThrowIfNull(corsOptions);

const string MyCorsPolicy = "myCors";
builder.Services.AddCors((opt) =>
{
    opt.AddPolicy(
        name: MyCorsPolicy,
        (policy) =>
        {
            policy.WithOrigins(corsOptions.AllowedOrigins)
                  .WithHeaders(corsOptions.AllowedHeaders)
                  .WithMethods(corsOptions.AllowedMethods);
        });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors(MyCorsPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
