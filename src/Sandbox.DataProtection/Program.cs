namespace Sandbox.DataProtection;

using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddJsonConsole(opt =>
        {
            opt.JsonWriterOptions = new JsonWriterOptions
            {
                Indented = true,
            };
        });
        builder.Logging.EnableRedaction();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.ExampleFilters();
        });
        builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        builder.Services.AddRedaction(opt =>
        {
            opt.SetRedactor<ErasingRedactor>(new DataClassificationSet(MyDataTaxonomy.SensitiveData));
            opt.SetRedactor<EmailMaskRedactor>(new DataClassificationSet(MyDataTaxonomy.EmailData));
            opt.SetHmacRedactor(opts =>
            {
                opts.Key = Convert.ToBase64String("ThisIsVerySecureKeyAndItMustBeGreaterThan44CharsLong!"u8);
                opts.KeyId = 42;
            }, new DataClassificationSet(MyDataTaxonomy.PiiData));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
