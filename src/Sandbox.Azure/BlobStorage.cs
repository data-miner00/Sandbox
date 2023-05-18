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

    public class BlobStorage
    {
        private readonly HttpClient httpClient;
        private readonly BlobStorageOptions options;

        public BlobStorage(HttpClient httpClient, BlobStorageOptions options)
        {
            this.httpClient = httpClient;
            this.options = options;
        }

        public async Task<BlobContainerClient> GetBlobStorageContainerAsync(string containerName)
        {
            var serviceClient = new BlobServiceClient(this.options.StorageConnectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

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

        public async Task EnumerateContainerAsync(BlobServiceClient client)
        {
            await foreach (BlobContainerItem container in client.GetBlobContainersAsync())
            {
                await Console.Out.WriteLineAsync($"Container name: {container.Name}");
            }
        }
    }
}
