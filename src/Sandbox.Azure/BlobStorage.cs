namespace Sandbox.Azure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Flurl;
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

        public async Task<BlobContainerClient> GetBlobStorageContainer(string containerName)
        {
            var serviceClient = new BlobServiceClient(this.options.StorageConnectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

        public async Task<IEnumerable<string>> GetItemUrlInTheContainer()
        {
            var containerClient = await this.GetBlobStorageContainer(this.options.ContainerName);
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

        public async Task UploadBinaryFileToBlob()
        {
            // Retrieves stream from somewhere, e.g. `Request.Body`.
            var content = Stream.Null;

            var containerClient = await this.GetBlobStorageContainer(this.options.ContainerName);
            var blobName = Guid.NewGuid().ToString().ToLower().Replace("-", string.Empty);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content);
            Console.Out.WriteLine($"Upload successful: {blobName}");
        }
    }
}
