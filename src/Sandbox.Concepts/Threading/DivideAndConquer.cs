namespace Sandbox.Concepts.Threading
{
    using System;
    using System.Diagnostics;
    using Sandbox.Core;

    internal class DivideAndConquer : IDemo
    {
        public void Demo()
        {
            this.NormalSum();
            this.DivideAndConquerSum();

            /*
             * Summing...
             * Total is: 2249927384
             * Sum elapsed: 00:00:00.5234407
             * Summing...
             * Total is: 2249927384
             * Sum elapsed: 00:00:00.1718889
             */
        }

        private void NormalSum()
        {
            var values = new byte[500000000];

            var rand = new Random(987);

            for (var i = 0; i < values.Length; i++)
            {
                values[i] = (byte)rand.Next(10);
            }

            Console.WriteLine("Summing...");
            long total = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < values.Length; i++)
            {
                total += values[i];
            }

            stopwatch.Stop();
            Console.WriteLine("Total is: " + total);
            Console.WriteLine("Sum elapsed: " + stopwatch.Elapsed);
        }

        private void DivideAndConquerSum()
        {
            var values = new byte[500000000];
            var portionResults = new long[Environment.ProcessorCount];
            var portionSize = values.Length / Environment.ProcessorCount;
            var rand = new Random(987);

            for (var i = 0; i < values.Length; i++)
            {
                values[i] = (byte)rand.Next(10);
            }

            Console.WriteLine("Summing...");
            long total = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var threads = new Thread[Environment.ProcessorCount];
            for (var i = 0; i < Environment.ProcessorCount; i++)
            {
                threads[i] = new Thread(SumPortion);
                threads[i].Start(i);
            }

            for (var i = 0; i < Environment.ProcessorCount; i++)
            {
                threads[i].Join();
            }

            for (var i = 0; i < Environment.ProcessorCount; i++)
            {
                total += portionResults[i];
            }

            stopwatch.Stop();
            Console.WriteLine("Total is: " + total);
            Console.WriteLine("Sum elapsed: " + stopwatch.Elapsed);

            void SumPortion(object portionNumber)
            {
                long sum = 0;
                int portionNumberAsInt = (int)portionNumber;
                int baseIndex = portionNumberAsInt * portionSize;
                for (var i = baseIndex; i < baseIndex + portionSize; i++)
                {
                    sum += values[i];
                }

                portionResults[portionNumberAsInt] = sum;
            }
        }
    }
}
