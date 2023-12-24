namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public static class ProcessesExamples
    {
        public static void Examples()
        {
            Process process = null!;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                process = Process.Start("notepad.exe", "random_words.txt");
            }
            else
            {
                process = Process.Start("nvim", "random_words.txt");
            }

            // Kill a process
            Thread.Sleep(3000);
            process.Kill();

            var processes = Process.GetProcessesByName("notepad");
            Console.WriteLine("{0} Notepad processes", processes.Length);

            foreach (var proces in processes)
            {
                Console.WriteLine(proces.ProcessName, proces.Id);
            }
        }
    }
}
