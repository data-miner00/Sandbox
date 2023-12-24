namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Diagnostics.Metrics;

    public static class Metrics
    {
        public static void Example()
        {
            Meter meter = new("Sandbox.Concepts.Bcl", "1.0.0");
            var totalSold = meter.CreateCounter<int>("sold");

            while (!Console.KeyAvailable)
            {
                // Emulating the transaction each second that sells 4 hats
                Thread.Sleep(1000);
                totalSold.Add(4);
            }

            // dotnet add package System.Diagnostics.DiagnosticSource
            // dotnet run (don't exit)

            // new console
            // dotnet tool update -g dotnet-counters
            // dotnet-counters ps (processes that can be monitored)
            // dotnet-counters monitor -p 19964 Sandbox.Concepts.Bcl
        }
    }
}
