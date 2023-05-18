namespace Sandbox.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Sandbox.Core.Options;
    using Sandbox.Library.FSharp;

    public sealed class CosmosDb
    {
        private readonly CosmosOption option;
        private IEnumerable<Customer> customers;

        public CosmosDb(CosmosOption option)
        {
            this.option = option;
        }

        public CosmosClient InstantiateCosmosClient()
        {
            var clientOptions = new CosmosClientOptions { AllowBulkExecution = true };
            var cosmosClient = new CosmosClient(this.option.CosmosEndpoint, this.option.AuthorizationKey, clientOptions);

            return cosmosClient;
        }

        public async Task<Database> GetOrCreateDatabase()
        {
            var cosmosClient = this.InstantiateCosmosClient();
            var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(this.option.DatabaseName);

            return database;
        }

        public async Task<Container> CreateContainerWithPolicy(string containerName, string partitionKey)
        {
            var database = await this.GetOrCreateDatabase();

            var container = await database.DefineContainer(containerName, partitionKey)
                .WithIndexingPolicy()
                    .WithIndexingMode(IndexingMode.Consistent)
                    .WithIncludedPaths()
                        .Attach()
                    .WithExcludedPaths()
                        .Path("/*")
                        .Attach()
                .Attach()
                .CreateAsync();

            return container;
        }

        public async Task ReadData(string path)
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(path)))
            {
                string json = await reader.ReadToEndAsync();
                this.customers = JsonSerializer.Deserialize<List<Customer>>(json)!;
            }
        }

        public async Task InsertIntoCosmos()
        {
            var stopwatch = Stopwatch.StartNew();
            var container = await this.CreateContainerWithPolicy("Costomer", "/FirstName");

            var tasks = new List<Task>(this.customers.Count());

            foreach (var custom in this.customers)
            {
                tasks.Add(container.CreateItemAsync(custom, new PartitionKey(custom.FirstName))
                    .ContinueWith(itemResponse =>
                    {
                        if (!itemResponse.IsCompletedSuccessfully)
                        {
                            AggregateException innerExceptions = itemResponse.Exception.Flatten();
                            if (innerExceptions.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                            {
                                Console.WriteLine($"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                            }
                            else
                            {
                                Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                            }
                        }
                    }));
            }

            await Task.WhenAll(tasks);
            stopwatch.Stop();

            Console.WriteLine("Execution time: {0}", stopwatch.Elapsed);
        }
    }
}
