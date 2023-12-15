namespace Sandbox.Concepts.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class Compression
    {
        public static void ZipDirExample()
        {
            var startPath = @".\toZip";
            var zipPath = @".\compressed.zip";
            var extractPath = @"\.extract";

            // Zipping a directory
            ZipFile.CreateFromDirectory(startPath, zipPath);

            // Unzipping to a directory
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }
    }
}
