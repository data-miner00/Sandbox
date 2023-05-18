namespace Sandbox.Core.Options
{
    using System.ComponentModel.DataAnnotations;

    public sealed class CosmosOption
    {
        [Required]
        public string DatabaseName { get; set; }

        [Required]
        public string AuthorizationKey { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string CosmosEndpoint { get; set; }
    }
}
