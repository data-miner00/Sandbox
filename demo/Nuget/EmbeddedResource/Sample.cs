using System.Reflection;

namespace Sandbox.Nuget.NetCore.EmbeddedResource
{
    public class Sample
    {
        private const string ResourceFileName = "resource.txt";

        public static string Resource
        {
            get
            {
                return ReadResource(ResourceFileName);
            }
        }

        private static string ReadResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(name));

            using Stream stream = assembly.GetManifestResourceStream(resourcePath)
                ?? throw new InvalidOperationException("File not found.");
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
