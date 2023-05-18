namespace Sandbox.Core.Options
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The options for Blob Storage access.
    /// </summary>
    public class BlobStorageOptions
    {
        /// <summary>
        /// Gets or sets the storage name.
        /// </summary>
        [Required]
        public string StorageName { get; set; }

        /// <summary>
        /// Gets or sets the storage key.
        /// </summary>
        [Required]
        public string StorageKey { get; set; }

        /// <summary>
        /// Gets or sets the storage connection string.
        /// </summary>
        [Required]
        public string StorageConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the container name.
        /// </summary>
        [Required]
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the Blob Service endpoint.
        /// </summary>
        [Required]
        public string BlobServiceEndpoint { get; set; }
    }
}
