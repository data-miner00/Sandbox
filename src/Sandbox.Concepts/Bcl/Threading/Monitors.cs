namespace Sandbox.Concepts.Bcl.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class Monitors
    {
        public static void Example()
        {
            List<Task> tasks = [];
            Random random = new();
            var total = 0L;
            var n = 0;

            for (var i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    var values = new int[10_000];
                    var taskTotal = 0;
                    var taskN = 0;
                    var counter = 0;

                    Monitor.Enter(random);

                    for (counter = 0; counter < 10_000; counter++)
                    {
                        values[counter] = random.Next(0, 1_001);
                    }

                    Monitor.Exit(random);
                    taskN = counter;

                    foreach (var value in values)
                    {
                        taskTotal += value;
                    }

                    Console.WriteLine("Mean for task {0,2}: {1:N2} (N={2:N0})", Task.CurrentId, (taskTotal * 1.0) / taskN, taskN);
                    Interlocked.Add(ref n, taskN);
                    Interlocked.Add(ref total, taskTotal);
                }));
            }

            try
            {
                Task.WaitAll([.. tasks]);
                Console.WriteLine("Mean for all tasks: {0:N2} (N={1:N0})", (total * 1.0) / n, n);
            }
            catch (AggregateException ex)
            {
                foreach (var ie in ex.InnerExceptions)
                {
                    Console.WriteLine("{0}: {1}", ie.GetType().Name, ie.Message);
                }
            }
        }
    }
}
