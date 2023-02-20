namespace Sandbox.ConsoleApp
{
    using System;
    using Sandbox.Core;

    internal class FileSystem : IDemo
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
