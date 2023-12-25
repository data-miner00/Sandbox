namespace Sandbox.Concepts.Bcl.Threading
{
    using System;
    using System.Threading;

    public static class ManualThreading
    {
        public static void Example()
        {
            var thread1 = new Thread(new ThreadStart(Thread1Work));
            var thread2 = new Thread(new ThreadStart(Thread2Work));

            Console.WriteLine("Thread 1 state: {0}", thread1.ThreadState.ToString());
            Console.WriteLine("Thread 2 state: {0}", thread2.ThreadState.ToString());

            thread1.Start();
            thread2.Start();

            Console.WriteLine("Thread 1 state: {0}", thread1.ThreadState.ToString());
            Console.WriteLine("Thread 2 state: {0}", thread2.ThreadState.ToString());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Main thread: Do work - Iteration {0}", i);
                Thread.Sleep(2000);
            }

            // Total 3 threads working now
        }

        public static void ThreadJoin()
        {
            var thread1 = new Thread(new ThreadStart(Thread1Work));
            var thread2 = new Thread(new ThreadStart(Thread2Work));

            Console.WriteLine("Main thread: Call Join() and wait until ThreadProc ends.");

            thread1.Start();
            thread1.Join();

            thread2.Start();
            thread2.Join();

            Console.WriteLine("Thread 1 state: {0}", thread1.ThreadState.ToString());
            Console.WriteLine("Thread 2 state: {0}", thread2.ThreadState.ToString());

            Console.WriteLine("Main thread: All threads finished executing.");
        }

        private static void Thread1Work()
        {
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine("Thread 1 executing iteration {0}", i);
                Thread.Sleep(1000);
            }
        }

        private static void Thread2Work()
        {
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine("Thread 2 executing iteration {0}", i);
                Thread.Sleep(1000);
            }
        }
    }
}
