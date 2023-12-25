namespace Sandbox.Concepts.Bcl.Threading
{
    using System;
    using System.Threading;

    public static class MutexExamples
    {
        public static void Example()
        {
            Mutex mutex = new();
            const int iterations = 1;
            const int threadCount = 3;

            for (var i = 0; i < threadCount; i++)
            {
                Thread thread = new(new ThreadStart(ThreadProcedure))
                {
                    Name = string.Format("Thread{0}", i + 1),
                };
                thread.Start();
            }

            Console.WriteLine("Main thread exits. Application continues to run until all foreground threads have exited");

            void ThreadProcedure()
            {
                for (var i = 0; i < iterations; i++)
                {
                    UseResource();
                }
            }

            void UseResource()
            {
                Console.WriteLine("{0} is reqesting the mutex", Thread.CurrentThread.Name);
                mutex.WaitOne();

                Console.WriteLine("{0} has entered the protected area", Thread.CurrentThread.Name);

                // working
                Thread.Sleep(1000);

                Console.WriteLine("{0} is leaving the protected area", Thread.CurrentThread.Name);
                mutex.ReleaseMutex();
                Console.WriteLine("{0} has released the mutex", Thread.CurrentThread.Name);
            }
        }
    }
}
