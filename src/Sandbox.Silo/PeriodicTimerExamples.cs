namespace Sandbox.Silo
{
    using System;
    using System.Threading.Tasks;
    using System.Timers;

    internal static class PeriodicTimerExamples
    {
        public static async Task IgnoreTimerPeriod()
        {
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));
            var i = 0;

            while (await timer.WaitForNextTickAsync() && i < 5)
            {
                await Console.Out.WriteLineAsync($"Execution #{i + 1} ran at: {DateTime.Now:hh:mm:ss fff}");
                i++;
                await Task.Delay(3000); // even the timer was set to execute every 1 second, it will actually wait for the code to finish execute
            }

            await Console.Out.WriteLineAsync("We are done");
        }

        public static async Task ConfigureTimerPeriod()
        {
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));
            var i = 0;

            while (await timer.WaitForNextTickAsync() && i < 5)
            {
                await Console.Out.WriteLineAsync($"Execution #{i + 1} ran at: {DateTime.Now:hh:mm:ss fff}");
                i++;
                timer.Period = TimeSpan.FromSeconds(i);
            }

            await Console.Out.WriteLineAsync("We are done");
        }

        public static async Task TurningTimerOff()
        {
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

            timer.Period = Timeout.InfiniteTimeSpan;

            await timer.WaitForNextTickAsync();
        }

        public static void RegularTimers()
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += MyElapsed;
            timer.Elapsed += MyElapsed2;

            timer.Start();

            Console.ReadKey();

            timer.Elapsed -= MyElapsed2;

            static void MyElapsed(object? sender, ElapsedEventArgs e)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Elapsed: {0:HH:mm:ss.fff}", e.SignalTime);
            }

            static void MyElapsed2(object? sender, ElapsedEventArgs e)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Elapsed2: {0:HH:mm:ss.fff}", e.SignalTime);
            }
        }
    }
}
