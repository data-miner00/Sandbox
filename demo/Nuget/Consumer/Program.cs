using Sandbox.Nuget.NetCore.EmbeddedResource;

namespace Sandbox.Nuget.NetCore.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Sample.Resource);
        }
    }
}
