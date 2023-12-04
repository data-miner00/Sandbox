namespace Sandbox.Concepts
{
    using System;

    internal class ArrayExamples
    {
        public static void Arrays()
        {
            var someArray = new[] { 1, 2, 3, 4 };
            var emptyArray = new int[0] { };

            Array.Resize(ref emptyArray, 3);
            Array.Copy(someArray, emptyArray, 3);
        }

        public static void Rank()
        {
            int[,] twoDimension = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Console.WriteLine(twoDimension.Rank);

            for (var outer = twoDimension.GetLowerBound(0); outer <= twoDimension.GetUpperBound(0); outer++)
            {
                for (var inner = twoDimension.GetLowerBound(1); inner <= twoDimension.GetUpperBound(1); inner++)
                {
                    Console.WriteLine($"{outer}, {inner}");
                }
            }
        }
    }
}
