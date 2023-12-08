namespace Sandbox.Concepts.Bcl
{
    using System.Collections;
    using System.Collections.Generic;

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

        public static void HashSetExamples()
        {
            HashSet<int> evenNumbers = [];

            for (var i = 0; i < 10; i++)
            {
                evenNumbers.Add(i * 2);
            }
        }
    }
}
