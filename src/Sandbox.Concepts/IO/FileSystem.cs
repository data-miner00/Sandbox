namespace Sandbox.Concepts.IO
{
    using System;

    internal class FileSystem
    {
        public void Demo()
        {
            using var reader = File.OpenText("sometext.txt");

            var line = reader.ReadLine();

            while (line != null)
            {
                // do something with line
                Console.WriteLine(line);

                line = reader.ReadLine();
            }
        }
    }
}
