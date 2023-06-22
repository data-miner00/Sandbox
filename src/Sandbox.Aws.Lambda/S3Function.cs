namespace Sandbox.Aws.Lambda;

using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;

/// <summary>
/// The sample class that uses S3 with Lambda.
/// </summary>
internal class S3Function
{
    /// <summary>
    /// Initializes a new instance of the <see cref="S3Function"/> class.
    /// </summary>
    public S3Function()
    {
        this.S3Client = new AmazonS3Client();
    }

    private IAmazonS3 S3Client { get; set; }

    /// <summary>
    /// A handler that resizes an image when the image is uploaded to S3 Bucket.
    /// </summary>
    /// <param name="event">The image uploaded event.</param>
    /// <param name="context">The context of lambda.</param>
    /// <returns>Nothing.</returns>
    public async Task FunctionHandler(S3Event @event, ILambdaContext context)
    {
        var eventRecords = @event.Records ?? new List<S3Event.S3EventNotificationRecord>();

        foreach (var s3Event in from record in eventRecords
                                let s3Event = record.S3
                                select s3Event)
        {
            if (s3Event == null)
            {
                continue;
            }

            try
            {
                var response = await this.S3Client.GetObjectMetadataAsync(s3Event.Bucket.Name, s3Event.Object.Key);

                if (response.Metadata["x-amz-meta-resized"] == true.ToString())
                {
                    context.Logger.LogInformation($"Item with key {s3Event.Object.Key} is already resized");
                    continue;
                }

                await using var itemStream = await this.S3Client.GetObjectStreamAsync(
                    s3Event.Bucket.Name,
                    s3Event.Object.Key,
                    new Dictionary<string, object>());

                using var outStream = new MemoryStream();
                using var image = await Image.LoadAsync(itemStream);

                image.Mutate(x => x.Resize(500, 500, KnownResamplers.Lanczos3));
                var originalName = response.Metadata["x-amz-meta-originalname"];

                await image.SaveAsync(outStream, image.DetectEncoder(originalName));

                await this.S3Client.PutObjectAsync(new PutObjectRequest
                {
                    BucketName = s3Event.Bucket.Name,
                    Key = s3Event.Object.Key,
                    Metadata =
                    {
                        ["x-amz-meta-originalname"] = response.Metadata["x-amz-meta-originalname"],
                        ["x-amz-meta-extension"] = response.Metadata["x-amz-meta-extension"],
                        ["x-amz-meta-resized"] = true.ToString(),
                    },
                    ContentType = response.Headers["Content-Type"],
                    InputStream = outStream,
                });

                context.Logger.LogInformation($"Resized image with key: {s3Event.Object.Key}");
            }
            catch (Exception ex)
            {
                context.Logger.LogError(
                    $"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and bucket is same region with the function.");
                context.Logger.LogError(ex.Message);
                context.Logger.LogError(ex.StackTrace);
            }
        }
    }
}
