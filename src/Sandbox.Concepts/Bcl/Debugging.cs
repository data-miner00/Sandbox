namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Diagnostics;

    public static class Debugging
    {
        public static void TraceExamples(string filename)
        {
            var file = File.Create(filename);

            Debug.AutoFlush = true;
            Debug.Indent();
            Debug.WriteLine("Hello from debug");
            Debug.Unindent();

            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TextWriterTraceListener textListener = new(file);
            Trace.Listeners.Add(textListener);
            Trace.AutoFlush = true;
            Trace.Indent();
            Trace.WriteLine("Hello from trace");
            Trace.Unindent();

            Random random = new();
            var len = random.Next(10, 50);

            Debug.WriteLine($"[Debug] Generating {len} random numbers");
            Trace.WriteLine($"[Trace] Generating {len} random numbers");

            var arr = new int[len];

            for (var i = 0; i < len; i++)
            {
                arr[i] = random.Next(1000, 9999);
            }

            Debug.WriteLine($"[Debug] Array: {arr}");
            Trace.WriteLine($"[Trace] Array: {arr}");

            Stopwatch sw = new();

            sw.Start();
            Console.WriteLine("Hello");
            sw.Stop();

            var ts = sw.Elapsed;

            Debug.WriteLine($"[Debug] X takes {ts.TotalMilliseconds} seconds");
            Trace.WriteLine($"[Trace] X takes {ts.TotalMilliseconds} seconds");

            sw.Reset();
        }
    }
}
