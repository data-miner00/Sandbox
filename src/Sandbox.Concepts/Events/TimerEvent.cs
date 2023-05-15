namespace Sandbox.Concepts.Events
{
    using System;
    using System.Timers;

    /// <summary>
    /// A sample class to showcase working with timer and events.
    /// </summary>
    internal static class TimerEvent
    {
        /// <summary>
        /// The main method that demonstrate adding and removing events from timer.
        /// </summary>
        public static void ChainFunctionToTimer()
        {
            var timer = new Timer(1000);

            // Adding events
            timer.Elapsed += ExecutionFunction;
            timer.Elapsed += ExecutionFunction2;

            timer.Start();

            Console.ReadKey();

            // Removing event
            timer.Elapsed -= ExecutionFunction2;

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
            Console.WriteLine("Elapsed: {0:HH:mm:ss.fff}", e.SignalTime);
        }
    }
}
