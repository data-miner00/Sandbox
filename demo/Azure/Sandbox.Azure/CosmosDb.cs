namespace Sandbox.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;

    /// <summary>
    /// The class for documenting the usage for <see cref="CosmosClient"/>.
    /// </summary>
    file sealed class CosmosDb
    {
        private readonly Container container;
        private readonly CosmosOption option;
        private IEnumerable<Customer> customers = new List<Customer>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDb"/> class.
        /// </summary>
        /// <param name="option">The option for instantiating the <see cref="CosmosClient"/>.</param>
        public CosmosDb(CosmosOption option)
        {
            this.option = option;
            this.container = this.CreateContainerWithPolicy("Costomer", "/FirstName").GetAwaiter().GetResult();
        }

        /// <summary>
        /// Instantiating <see cref="CosmosClient"/> through endpoint and authorization key.
        /// </summary>
        /// <returns>The <see cref="CosmosClient"/>.</returns>
        public CosmosClient InstantiateCosmosClient()
        {
            var clientOptions = new CosmosClientOptions { AllowBulkExecution = true };
            var cosmosClient = new CosmosClient(this.option.CosmosEndpoint, this.option.AuthorizationKey, clientOptions);

            return cosmosClient;
        }

        /// <summary>
        /// Create or retrieve a database instance.
        /// </summary>
        /// <returns>The <see cref="Database"/> instance.</returns>
        public async Task<Database> GetOrCreateDatabase()
        {
            var cosmosClient = this.InstantiateCosmosClient();
            var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(this.option.DatabaseName);

            return database;
        }

        /// <summary>
        /// Create a container with policy.
        /// </summary>
        /// <param name="containerName">The name for the container.</param>
        /// <param name="partitionKey">The partition key used for the collection.</param>
        /// <returns>The <see cref="Container"/> instance.</returns>
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

        /// <summary>
        /// Read Json data from file and serialize it into object.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>Nothing.</returns>
        public async Task ReadData(string path)
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(path)))
            {
                string json = await reader.ReadToEndAsync();
                this.customers = JsonSerializer.Deserialize<List<Customer>>(json)!;
            }
        }

        /// <summary>
        /// Insert records into CosmosDb.
        /// </summary>
        /// <returns>Nothing.</returns>
        public async Task InsertIntoCosmos()
        {
            var stopwatch = Stopwatch.StartNew();

            var tasks = new List<Task>(this.customers.Count());

            foreach (var custom in this.customers)
            {
                tasks.Add(this.container.CreateItemAsync(custom, new PartitionKey(custom.FirstName))
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

        /// <summary>
        /// Find customer from CosmosDb.
        /// </summary>
        /// <param name="firstName">The first name of the customer.</param>
        /// <returns>The found <see cref="Customer"/> or default.</returns>
        public async Task<Customer> FindCustomerAsync(string firstName)
        {
            var iterator = this.container.GetItemLinqQueryable<Customer>()
                .Where(x => x.FirstName == firstName)
                .OrderByDescending(x => x.LastName)
                .ToFeedIterator<Customer>();

            var matches = new List<Customer>();
            while (iterator.HasMoreResults)
            {
                var next = await iterator.ReadNextAsync();
                matches.AddRange(next);
            }

            return matches.SingleOrDefault();
        }

        /// <summary>
        /// Get the list of customers.
        /// </summary>
        /// <returns>The list of customers.</returns>
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var query = $@"SELECT * FROM Costomer";
            var iterator = this.container.GetItemQueryIterator<Customer>(query);

            var matches = new List<Customer>();
            while (iterator.HasMoreResults)
            {
                var next = await iterator.ReadNextAsync();
                matches.AddRange(next);
            }

            return matches;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    file struct Customer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
