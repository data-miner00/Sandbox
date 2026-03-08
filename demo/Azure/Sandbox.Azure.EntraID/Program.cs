using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Sandbox.Azure.EntraID.Options;
using System.Text.Json;

namespace Sandbox.Azure.EntraID
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", optional: false, reloadOnChange: false)
                .AddJsonFile("settings.dev.json", optional: false, reloadOnChange: false)
                .Build();

            var options = configuration.GetSection(AppCredentials.SectionName).Get<AppCredentials>()
                ?? throw new InvalidOperationException("Missing app credentials.");

            var clientCredentials = new ClientSecretCredential(
                options.TenantId,
                options.ClientId,
                options.ClientSecret);

            using var serviceClient = new GraphServiceClient(clientCredentials);

            var repo = new UserRepository(serviceClient);

            var allUsers = await repo.GetAllAsync();

            foreach (var user in allUsers)
            {
                Console.WriteLine("Name: {0}, Created: {1}", user.DisplayName, user.CreatedDateTime);
            }
        }
    }
}
