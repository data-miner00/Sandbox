namespace Sandbox.Concepts.Bcl.Threading
{
    using System;
    using System.Threading;

    public static class SemaphoreExamples
    {
        public static void Example()
        {
            var semaphorePool = new Semaphore(0, 3);
            int _padding = 0;

            // Create and start 5 threads
            for (var i = 0; i < 5; i++)
            {
                Thread thread = new(new ParameterizedThreadStart(Worker));
                thread.Start(i);
            }

            // Wait for all threads to start and block on the semaphore
            Thread.Sleep(1000);

            // The main thread manage the semaphore count.
            // Calling release(3) brings to original maximum value
            semaphorePool.Release(3);

            Console.WriteLine("Main thread exited");

            void Worker(object num)
            {
                // Requesting the semaphore
                Console.WriteLine("Thread {0} begins", num);
                semaphorePool.WaitOne();

                // Padding interval to make output to follow order
                var padding = Interlocked.Add(ref _padding, 100);
                Console.WriteLine("Thread {0} enters the semaphore", num);

                // Each thread words a little longer to make the output in order.
                Thread.Sleep(1000 + padding);
                Console.WriteLine("Thread {0} releases the semaphore", num);
                Console.WriteLine("Thread {0} previous semaphore count: {1}", num, semaphorePool.Release());
            }
        }
    }
}
