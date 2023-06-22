namespace Sandbox.Aws;

using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System;
using System.Threading.Tasks;

/// <summary>
/// The demo class for AWS Secrets manager code.
/// </summary>
internal class SecretsManager
{
    private readonly IAmazonSecretsManager secretsManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecretsManager"/> class.
    /// </summary>
    /// <param name="secretsManager">The <see cref="AmazonSecretsManagerClient"/> instance.</param>
    public SecretsManager(IAmazonSecretsManager secretsManager)
    {
        this.secretsManager = secretsManager;
    }

    /// <summary>
    /// Retrieving a secret value from the Secrets Manager.
    /// </summary>
    /// <param name="secretId">The ID for the secret.</param>
    /// <returns>Nothing.</returns>
    public async Task RetrieveSecret(string secretId)
    {
        var request = new GetSecretValueRequest
        {
            SecretId = secretId,
            VersionStage = "AWSPREVIOUS", // or AWSCURRENT
        };

        var response = await this.secretsManager.GetSecretValueAsync(request);

        Console.WriteLine(response.SecretString);
    }

    /// <summary>
    /// Retrieves all sorts of info and metadata for a secret. Does not include its
    /// actual value.
    /// </summary>
    /// <param name="secretId">The ID for the secret.</param>
    /// <returns>Nothing.</returns>
    public async Task DescribeSecret(string secretId)
    {
        var request = new DescribeSecretRequest
        {
            SecretId = secretId,
        };

#pragma warning disable S1481
        var response = await this.secretsManager.DescribeSecretAsync(request);
#pragma warning restore S1481
    }

    /// <summary>
    /// Retrieves all the versions of a secrets including the deprecated ones.
    /// </summary>
    /// <param name="secretId">The ID for the secret.</param>
    /// <returns>Nothing.</returns>
    public async Task ListSecretVersions(string secretId)
    {
        var request = new ListSecretVersionIdsRequest
        {
            SecretId = secretId,
            IncludeDeprecated = true,
        };

#pragma warning disable S1481
        var response = await this.secretsManager.ListSecretVersionIdsAsync(request);
#pragma warning restore S1481

        // Can use auto rotation
    }
}
