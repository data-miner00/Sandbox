namespace Sandbox.Concepts.IO
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary>
    /// The examples for I/O operations.
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    internal class Examples
    {
        public static void WriteStringsToFile(string filename)
        {
            File.WriteAllText(filename, "My string contents");
        }

        /// <summary>
        /// Read all the contents in a file into a big string.
        /// </summary>
        /// <param name="filename">The file npath.</param>
        public static void ReadContentFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Not found");
                return;
            }

            var content = File.ReadAllText(filename);
        }

        /// <summary>
        /// Can use this method to validate a given file path.
        /// </summary>
        public static void GetInvalidCharsForPath()
        {
            var invalidChars = Path.GetInvalidPathChars();

            foreach (var ch in invalidChars)
            {
                Console.Write(ch + " ");
            }
        }

        /// <summary>
        /// Writes each string element in an array into newline.
        /// </summary>
        /// <param name="filename">The file path.</param>
        public static void UsingWriteAllLines(string filename)
        {
            var lines = new[] { "hello", "world", "bello", "borld" };
            File.WriteAllLines(filename, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Append the text content at the end of the given file.
        /// </summary>
        /// <param name="filename">The file path.</param>
        public static void AppendText(string filename)
        {
            var appendText = "This is extra text" + Environment.NewLine;
            File.AppendAllText(filename, appendText, Encoding.UTF8);
        }

        /// <summary>
        /// Each individual line is loaded into an array of string.
        /// </summary>
        /// <param name="filename">The file path.</param>
        public static void UsingReadAllLines(string filename)
        {
            var lines = File.ReadAllLines(filename, Encoding.UTF8);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Read the contents into an array of bytes.
        /// </summary>
        /// <param name="filename">The file path.</param>
        public static void UsingReadAllBytes(string filename)
        {
            var bytes = File.ReadAllBytes(filename);

            foreach (var byyte in bytes)
            {
                Console.WriteLine($"{byyte}, ");
            }
        }

        /// <summary>
        /// Getting useful information for a file.
        /// </summary>
        public static void FileInfoExamples()
        {
            var filePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
            {
                Console.WriteLine(
                    string.Format(
                        "File Information: {0}, {1} bytes, last modified on {2} - Full path: {3}",
                        fileInfo.Name,
                        fileInfo.Length,
                        fileInfo.LastWriteTime,
                        fileInfo.FullName));
            }
        }

        private string GetDebuggerDisplay() => this.ToString() ?? nameof(Examples);
    }
}
