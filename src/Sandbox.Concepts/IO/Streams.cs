namespace Sandbox.Concepts.IO
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Examples for working with streams.
    /// </summary>
    internal static class Streams
    {
        /// <summary>
        /// Example for using File.OpenWrite.
        /// </summary>
        /// <param name="filename">The file path.</param>
        public static void WriteFileStream(string filename)
        {
            using var wfs = File.OpenWrite(filename);

            var data = "hello\nworld\n\nwelcome";
            var bytes = Encoding.UTF8.GetBytes(data);

            wfs.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Example for using File.OpenRead.
        /// </summary>
        /// <param name="filename">The file path.</param>
        public static void ReadFileStream(string filename)
        {
            using var rfs = File.OpenRead(filename);
            var buf = new byte[1024];
            int c;

            while ((c = rfs.Read(buf, 0, buf.Length)) > 0)
            {
                Console.WriteLine(Encoding.UTF8.GetString(buf, 0, c));
            }
        }

        /// <summary>
        /// Example for using MemoryStream with bytes and StreamReader.
        /// </summary>
        /// <param name="filename">The file path.</param>
        public static void ReadMemoryStream(string filename)
        {
            var contentBuf = File.ReadAllBytes(filename);
            using var memoryStream = new MemoryStream(contentBuf);
            using var textReader = new StreamReader(memoryStream);

            var line = string.Empty;

            while ((line = textReader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Example for downloading an image from the internet.
        /// </summary>
        /// <param name="imageUrl">The url for the image.</param>
        /// <returns>The task.</returns>
        public static async Task DownloadImage(string imageUrl)
        {
            using var httpClient = new HttpClient();
            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

            using var fs = new FileStream("img.jpg", FileMode.Create);
            fs.Write(imageBytes, 0, imageBytes.Length);
        }

        /// <summary>
        /// Example for reading an image as bytes.
        /// </summary>
        /// <param name="filename">The file path.</param>
        public static void ViewImage(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open);

            int c, i = 0;

            while ((c = fs.ReadByte()) != -1)
            {
                Console.WriteLine("{0:X2}", c);
                i++;

                if (i % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
