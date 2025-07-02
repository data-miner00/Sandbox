namespace Sandbox.Azure
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The option class that contains configurations used for Cosmos DB.
    /// </summary>
    public sealed class CosmosOption
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        [Required]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the access key of the Cosmos DB.
        /// </summary>
        [Required]
        public string AuthorizationKey { get; set; }

        /// <summary>
        /// Gets or sets the connection string of the Cosmos DB.
        /// </summary>
        [Required]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the URI endpoint of the Cosmos DB.
        /// </summary>
        [Required]
        public string CosmosEndpoint { get; set; }
    }
}
