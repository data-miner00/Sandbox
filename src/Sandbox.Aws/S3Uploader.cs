namespace Sandbox.Aws;

using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

internal class S3Uploader
{
    private readonly IAmazonS3 s3Client;

    public S3Uploader(IAmazonS3 s3Client)
    {
        this.s3Client = s3Client;
    }

    public async Task UploadItem()
    {
        await using var inputStream = new FileStream("./my/path/to/item.jpg", FileMode.Open, FileAccess.Read);

        var putObjectRequest = new PutObjectRequest
        {
            BucketName = "mybucket",
            Key = "my/path/to/item.jpg",
            ContentType = "image/jpeg",
            InputStream = inputStream,
        };

        await this.s3Client.PutObjectAsync(putObjectRequest);
    }
}
