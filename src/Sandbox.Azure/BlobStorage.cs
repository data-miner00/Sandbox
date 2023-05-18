namespace Sandbox.Azure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using global::Azure.Storage;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;
    using Sandbox.Core.Options;

    /// <summary>
    /// A class for documenting the Azure Blob Storage SDK.
    /// </summary>
    public sealed class BlobStorage
    {
        private readonly HttpClient httpClient;
        private readonly BlobStorageOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobStorage"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> responsible for making HTTP requests.</param>
        /// <param name="options">The <see cref="BlobStorageOptions"/> that contains the credentials for creating a connection to Azure Blob Storage.</param>
        public BlobStorage(HttpClient httpClient, BlobStorageOptions options)
        {
            this.httpClient = httpClient;
            this.options = options;
        }

        /// <summary>
        /// Retrieves the Blob Storage Container by it's name. If not exist, it will create the container instead.
        /// </summary>
        /// <param name="containerName">The name of the container to be create/retrieved.</param>
        /// <returns>The <see cref="BlobContainerClient"/> object for that particular container.</returns>
        public async Task<BlobContainerClient> GetBlobStorageContainerAsync(string containerName)
        {
            var serviceClient = new BlobServiceClient(this.options.StorageConnectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

        /// <summary>
        /// Retrieves a list of Uri for the items within the container.
        /// </summary>
        /// <returns>The list of Uri for items found in the container.</returns>
        public async Task<IEnumerable<string>> GetItemUrlInTheContainerAsync()
        {
            var containerClient = await this.GetBlobStorageContainerAsync(this.options.ContainerName);
            var results = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                results.Add(
                    Flurl.Url.Combine(
                        containerClient.Uri.AbsoluteUri,
                        blobItem.Name));

                Console.Out.WriteLine($"Item found: {blobItem.Name}");
            }

            return results;
        }

        /// <summary>
        /// Uploads a blob from the binary stream to the Blob Storage.
        /// </summary>
        /// <returns>Nothing.</returns>
        public async Task UploadBinaryFileToBlobAsync()
        {
            // Retrieves stream from somewhere, e.g. `Request.Body`.
            var content = Stream.Null;

            var containerClient = await this.GetBlobStorageContainerAsync(this.options.ContainerName);
            var blobName = Guid.NewGuid().ToString().ToLower().Replace("-", string.Empty);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content);
            Console.Out.WriteLine($"Upload successful: {blobName}");
        }

        /// <summary>
        /// Another way of instantiating the <see cref="BlobServiceClient"/> with storage name, key and it's endpoint.
        /// </summary>
        /// <returns>Nothing.</returns>
        public async Task InstantiateWithKeyAndUrlAsync()
        {
            var accountCredentials = new StorageSharedKeyCredential(this.options.StorageName, this.options.StorageKey);
            var serviceClient = new BlobServiceClient(new Uri(this.options.BlobServiceEndpoint), accountCredentials);

            var response = await serviceClient.GetAccountInfoAsync();
            var info = response.Value;

            await Console.Out.WriteLineAsync("Connected to Azure Storage Account");
            await Console.Out.WriteLineAsync($"Account name: {this.options.StorageName}");
            await Console.Out.WriteLineAsync($"Account kind: {info.AccountKind}");

            // Stock Keeping Unit (SKU)
            await Console.Out.WriteLineAsync($"Account sku: {info.SkuName}");
        }

        /// <summary>
        /// Traverse through the found container and print it's corresponding name.
        /// </summary>
        /// <param name="client">The <see cref="BlobServiceClient"/> instance.</param>
        /// <returns>Nothing.</returns>
        public async Task EnumerateContainerAsync(BlobServiceClient client)
        {
            await foreach (BlobContainerItem container in client.GetBlobContainersAsync())
            {
                await Console.Out.WriteLineAsync($"Container name: {container.Name}");
            }
        }
    }
}
