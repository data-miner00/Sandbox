using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SQS;

// many ways to instantiate a sqs client
var sqsClient = new AmazonSQSClient(RegionEndpoint.AFSouth1);

var sqsClient2 = new AmazonSQSClient(new AmazonSQSConfig
{
    RegionEndpoint = RegionEndpoint.APNortheast1,
});

var snsClient = new AmazonSimpleNotificationServiceClient();
