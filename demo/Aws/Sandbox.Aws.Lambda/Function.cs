using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Sandbox.Aws.Lambda;

public class Function
{
    /// <summary>
    /// A simple function that takes a string and does a ToUpper.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="context">The lambda context.</param>
    /// <returns>Upper cased string.</returns>
    public string FunctionHandler(string input, ILambdaContext context) // The input can be an object deserialized from Json payload.
    {
        context.Logger.Log("Hello from C#");

        return input.ToUpper();
    }
}
