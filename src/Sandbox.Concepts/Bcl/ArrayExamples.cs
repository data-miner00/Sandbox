namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Collections;

    /// <summary>
    /// Some example operations involving arrays.
    /// </summary>
    internal static class ArrayExamples
    {
        /// <summary>
        /// Resize and copy nth element of an array.
        /// </summary>
        public static void Arrays()
        {
            var someArray = new[] { 1, 2, 3, 4 };
            var emptyArray = new int[0] { };

            Array.Resize(ref emptyArray, 3);
            Array.Copy(someArray, emptyArray, 3);
        }

        /// <summary>
        /// Multidimensional array examples.
        /// </summary>
        public static void Multidimension()
        {
            int[,] twoDimension =
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
            };

            Console.WriteLine(twoDimension.Rank);

            for (var outer = twoDimension.GetLowerBound(0); outer <= twoDimension.GetUpperBound(0); outer++)
            {
                for (var inner = twoDimension.GetLowerBound(1); inner <= twoDimension.GetUpperBound(1); inner++)
                {
                    Console.WriteLine($"{outer}, {inner}");
                }
            }
        }

        /// <summary>
        /// Bit array examples
        /// </summary>
        public static void Bits()
        {
            // Initialize bit array and initialize to true
            var ba = new BitArray(5, true);

            var bytes = new byte[5] { 1, 2, 3, 5, 3 };
            var casted = new BitArray(bytes);
        }
    }
}
