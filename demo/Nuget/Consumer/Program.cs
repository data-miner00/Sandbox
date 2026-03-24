using Sandbox.Nuget.MultiPlatform;
using Sandbox.Nuget.NetCore.EmbeddedResource;

namespace Sandbox.Nuget.NetCore.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Embedded Resource
            Console.WriteLine(Sample.Resource);

            // Multiplatform
            Console.WriteLine(Greetings.Hello);
        }
    }
}
