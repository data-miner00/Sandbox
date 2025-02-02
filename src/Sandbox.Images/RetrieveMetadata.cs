namespace Sandbox.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SixLabors.ImageSharp;

    internal static class RetrieveMetadata
    {
        public static void Retrieve(string filePath)
        {
            using var image = SixLabors.ImageSharp.Image.Load(filePath);

            var metadata = image.Metadata?.IptcProfile?.Values;

            if (metadata?.Any() ?? false)
            {
                foreach (var prop in metadata)
                {
                    Console.WriteLine($"{prop.Tag}: {prop.Value}");
                }
            }
        }
    }
}
