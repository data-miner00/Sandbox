namespace Sandbox.Nuget.NetCore.EmbeddedResource
{
    public class Sample
    {
        private const string ResourceFileName = "resource.txt";

        public static string Resource
        {
            get
            {
                return File.ReadAllText(ResourceFileName);
            }
        }
    }
}
