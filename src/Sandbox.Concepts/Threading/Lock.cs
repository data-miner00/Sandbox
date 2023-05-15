namespace Sandbox.Concepts
{
    using System;
    using Sandbox.Core;

    internal class Lock : IDemo
    {
        public void TestLock()
        {
            var obj = new object();

            for (var i = 0; i < 10; i++)
            {
                ThreadStart start = new ThreadStart(LockCode);
                new Thread(start).Start();
            }

            void LockCode()
            {
                lock (obj)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(Environment.TickCount);
                }
            }
        }

        public void TestPrintNoLock()
        {
            var thread1 = new Thread(PrintChar);
            var thread2 = new Thread(PrintChar);

            thread1.Start();
            thread2.Start();

            static void PrintChar()
            {
                var words = "This is a dummy sentence string";
                for (var i = 0; i < words.Length; i++)
                {
                    Console.Write($"{words[i]}");
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }

                Console.Write(" ");
            }
        }

        public void TestPrintWithLock()
        {
            var identity = new object();

            var thread1 = new Thread(PrintChar);
            var thread2 = new Thread(PrintChar);

            thread1.Start();
            thread2.Start();

            void PrintChar()
            {
                lock (identity)
                {
                    var words = "This is a dummy sentence string";
                    for (var i = 0; i < words.Length; i++)
                    {
                        Console.Write($"{words[i]}");
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }

                    Console.Write(" ");
                }
            }
        }

        public void Demo()
        {
            this.TestPrintWithLock();

            Console.ReadLine();
        }
    }
}
