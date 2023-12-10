namespace Sandbox.Concepts.Bcl
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    /// <summary>
    /// System.Collections examples class.
    /// </summary>
    internal static class CollectionsExamples
    {
        /// <summary>
        /// Use for comparing objects?.
        /// </summary>
        public static void DefaultComparer()
        {
            var str1 = new string('a', 3);
            var str2 = new string('b', 3);

            var result = Comparer.DefaultInvariant.Compare(str1, str2);
            Console.WriteLine($"{str1} == {str2} : {result}");
        }

        public static void HashtableExample()
        {
            var cars = new Hashtable
            {
                { "green", "Land Rover" },
                { "artic", "Frontier" },
                { "palomino", "Land Rover" },
                { "red", "Mustang" },
            };

            cars.Add("pink", "Urus");

            foreach (DictionaryEntry entry in cars)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }

            ICollection keys = cars.Keys;

            // Add method throws an exception if key is existing.
            try
            {
                cars.Add("pink", "Mustang");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Implemented with 2 arrays. One to store keys another to store value.
        /// </summary>
        public static void SortedListExamples()
        {
            var sl = new SortedList
            {
                { 3, "A" },
                { 5, "B" },
                { 1, "C" },
                { 2, "D" },
                { 8, "E" },
                { 6, "F" },
            };

            Console.WriteLine("Count: " + sl.Count);
            Console.WriteLine("Capacity: " + sl.Capacity);

            for (var i = 0; i < sl.Count; i++)
            {
                Console.WriteLine("{0}: {1}", sl.GetKey(i), sl.GetByIndex(i));
            }
        }

        /// <summary>
        /// Examples of enqueue, dequeue and peek for Queue.
        /// </summary>
        public static void QueueExamples()
        {
            // Implemented with circular array
            var queue = new Queue();

            queue.Enqueue("a");
            queue.Enqueue("b");
            queue.Enqueue("c");
            queue.Enqueue("d");
            queue.Enqueue("e");

            Console.WriteLine($"{queue.Dequeue()}");
            Console.WriteLine($"{queue.Peek()}");

            foreach (var obj in queue)
            {
                Console.WriteLine("{0} ", obj);
            }
        }

        /// <summary>
        /// Examples of push and pop operations on a stack.
        /// </summary>
        public static void StackExamples()
        {
            var stack = new Stack();

            stack.Push("a");
            stack.Push("b");
            stack.Push("c");
            stack.Push("d");
            stack.Push("e");

            Console.WriteLine($"{stack.Pop()}");
        }

        /// <summary>
        /// Examples of adding even numbers to a hash set.
        /// </summary>
        public static void HashSetExamples()
        {
            HashSet<int> evenNumbers = [];

            for (var i = 0; i < 10; i++)
            {
                evenNumbers.Add(i * 2);
            }
        }

        /// <summary>
        /// Examples of ConcurrentBag unordered collection.
        /// </summary>
        public static void ConcurrentBagExamples()
        {
            var cb = new ConcurrentBag<int>();

            var bagAddTasks = new List<Task>();

            for (var i = 0; i < 50; i++)
            {
                bagAddTasks.Add(Task.Run(() => cb.Add(i)));
            }

            Task.WaitAll([.. bagAddTasks]);

            Console.WriteLine("All tasks have completed");

            var bagConsumeTasks = new List<Task>();

            int itemsInBag = 0;

            while (!cb.IsEmpty)
            {
                bagConsumeTasks.Add(Task.Run(() =>
                {
                    if (cb.TryTake(out var item))
                    {
                        Console.WriteLine(item);
                        itemsInBag++;
                    }
                }));
            }

            Task.WaitAll([.. bagConsumeTasks]);
            Console.WriteLine($"There were {itemsInBag} items in bag");

            if (cb.TryPeek(out var item))
            {
                Console.WriteLine("Should not have items, found: " + item);
            }
        }

        public static void ConcurrentQueueExamples()
        {
            var cq = new ConcurrentQueue<int>();

            for (var i = 0; i < 10_000; i++)
            {
                cq.Enqueue(i);
            }

            if (cq.TryPeek(out var item))
            {
                Console.WriteLine("Try peek succes with " + item);
            }

            Console.WriteLine();

            var outerSum = 0;

            // An action to consume the cq
            var action = () =>
            {
                var localSum = 0;

                while (cq.TryDequeue(out var localValue))
                {
                    localSum += localValue;
                }

                Interlocked.Add(ref outerSum, localSum);
            };

            // Start 4 concurrent consuming actions
            Parallel.Invoke(action, action, action, action);

            Console.WriteLine("outerSum = {0}, should be 49995000", outerSum);
        }

        public static async Task ConcurrentStackExamples()
        {
            var items = 10_000;

            var cs = new ConcurrentStack<int>();

            var pusher = () =>
            {
                for (var i = 0; i < items; i++)
                {
                    cs.Push(i);
                }
            };

            pusher();

            if (cs.TryPeek(out var item))
            {
                Console.WriteLine("TryPeek() saw {0} on top of the stack", item);
            }

            // Clear the stack
            cs.Clear();

            if (cs.IsEmpty)
            {
                Console.WriteLine("The stack is empty");
            }

            var pushAndPop = () =>
            {
                Console.WriteLine("Task started on {0}", Task.CurrentId);

                for (var i = 0; i < items; i++)
                {
                    cs.Push(i);
                }

                for (var i = 0; i < items; ++i)
                {
                    cs.TryPop(out var item);
                }

                Console.WriteLine("Task ended on {0}", Task.CurrentId);
            };

            // Spin up five concurrent tasks of the action
            var tasks = new Task[5];

            for (var i = 0; i < tasks.Length; ++i)
            {
                tasks[i] = Task.Factory.StartNew(pushAndPop);
            }

            await Task.WhenAll(tasks);

            if (cs.IsEmpty)
            {
                await Console.Out.WriteLineAsync("Items all processed!");
            }
        }
    }
}
