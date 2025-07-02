namespace Sandbox.Aws;

using System;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

internal class S3Downloader
{
    private readonly IAmazonS3 s3Client;

    public S3Downloader(IAmazonS3 s3Client)
    {
        this.s3Client = s3Client;
    }

    public async Task DownloadItem()
    {
        var getObjectRequest = new GetObjectRequest
        {
            BucketName = "mybucket",
            Key = "cloud/path/to/my/item.jpg",
        };

        var response = await this.s3Client.GetObjectAsync(getObjectRequest);

        using var memoryStream = new MemoryStream();
        response.ResponseStream.CopyTo(memoryStream);

        var text = Encoding.Default.GetString(memoryStream.ToArray());

        await Console.Out.WriteLineAsync(text).ConfigureAwait(false);
    }
}
