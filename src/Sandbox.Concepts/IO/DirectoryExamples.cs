namespace Sandbox.Concepts.IO
{
    using System;
    using System.IO;

    /// <summary>
    /// Examples with directories.
    /// </summary>
    public static class DirectoryExamples
    {
        /// <summary>
        /// Creates a directory if not exist.
        /// </summary>
        /// <param name="newDir">The directory to be created.</param>
        public static void CreateDirectory(string newDir)
        {
            if (Directory.Exists(newDir))
            {
                Console.WriteLine("The directory already exist");
            }
            else
            {
                Directory.CreateDirectory(newDir);
            }
        }

        /// <summary>
        /// Create files inside the directory.
        /// </summary>
        /// <param name="newDir">The directory for the new files.</param>
        public static void CreateFilesInsideDirs(string newDir)
        {
            File.WriteAllText(
                $"{newDir}/newfile.txt",
                "Hello worlds!");
        }

        /// <summary>
        /// Creates sub directories.
        /// </summary>
        /// <param name="newDir">The new directory.</param>
        /// <param name="newSubDir">The sub directory within the new directory.</param>
        public static void CreateSubDirectory(string newDir, string newSubDir)
        {
            var fullPath = newDir + Path.DirectorySeparatorChar + newSubDir;
            Directory.CreateDirectory(fullPath);

            File.WriteAllText(fullPath, "Hello worlds!");
        }

        /// <summary>
        /// Gets the info of the directory.
        /// </summary>
        /// <param name="dir">The directory.</param>
        public static void GetDirectoryInfo(string dir)
        {
            var di = new DirectoryInfo(dir);

            // Get files in top directory only
            var subFiles = di.GetFiles();

            foreach (var subFile in subFiles)
            {
                Console.WriteLine(subFile.FullName + " (" + subFile.Length + " bytes)");
            }
        }

        /// <summary>
        /// Gets all text files within a directory including the sub directories.
        /// </summary>
        /// <param name="dir">The directory.</param>
        public static void GetAllDirectoryInfo(string dir)
        {
            var di = new DirectoryInfo(dir);
            var txtFiles = di.GetFiles("*.txt", SearchOption.AllDirectories);

            foreach (var txtFile in txtFiles)
            {
                Console.WriteLine(txtFile.Name + " (" + txtFile.Length + " bytes)");
            }
        }

        /// <summary>
        /// Move a directory to another directory.
        /// </summary>
        /// <param name="from">The origin.</param>
        /// <param name="to">The destination.</param>
        public static void MoveDirectory(string from, string to)
        {
            Directory.Move(from, to);
        }
    }
}
