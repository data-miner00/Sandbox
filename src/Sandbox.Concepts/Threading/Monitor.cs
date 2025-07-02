namespace Sandbox.Concepts.Threading
{
    using System;
    using System.Threading.Tasks;
    using Mon = System.Threading.Monitor;

    internal class Monitor
    {
        public void Demo()
        {
            int ntasks = 0;
            int total_tasks = 5;
            var identity = new object();

            Parallel.For(0, total_tasks, (i, state) =>
            {
                Thread.CurrentThread.Name = "thread-" + i;
                while (true)
                {
                    try
                    {
                        if (Mon.TryEnter(identity))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Working " + Thread.CurrentThread.Name);
                            ntasks++;
                            Thread.Sleep(100);
                            Mon.Exit(identity);
                            break;
                        }
                        else
                        {
                            Thread.Sleep(200);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Sleeping " + Thread.CurrentThread.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            });

            if (ntasks != total_tasks)
            {
                throw new Exception("Some task failed to execute. Total tasks executed: " + ntasks);
            }
        }

        private void SimpleDemo()
        {
            var lockObj = new object();
            var timeout = TimeSpan.FromMilliseconds(500);
            bool lockTaken = false;

            try
            {
                Mon.TryEnter(lockObj, timeout, ref lockTaken);

                if (lockTaken)
                {
                    // The critical section.
                }
                else
                {
                    // The lock was not acquired.
                }
            }
            finally
            {
                // Ensure that the lock is released.
                if (lockTaken)
                {
                    Mon.Exit(lockObj);
                }
            }
        }
    }
}
