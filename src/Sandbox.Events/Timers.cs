namespace Sandbox.Events
{
    using System;
    using System.Timers;

    internal static class Timers
    {
        public static void Demo()
        {
            Timer timer = new Timer(1000); // timer that runs every 1s

            timer.Elapsed += ExecutionFunction; // adding events
            timer.Elapsed += ExecutionFunction2;

            timer.Start();

            Console.ReadKey();

            timer.Elapsed -= ExecutionFunction2; // removing event

            Console.ReadKey();
        }

        private static void ExecutionFunction(object? sender, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Elapsed: {0:HH:mm:ss.fff}", e.SignalTime);
        }

        private static void ExecutionFunction2(object? sender, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Elapsed2: {0:HH:mm:ss.fff}", e.SignalTime);
        }
    }
}
