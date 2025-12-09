namespace Sandbox.ConsoleApp
{
    using System;
    using System.Text;
    using Sandbox.Concepts.Bcl.Threading;
    using Sandbox.Nuget.NetCore;

    /// <summary>
    /// A sandbox class to play around and experiment with.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point of the sandbox.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            var data = "hello, world";

            var binaryData = GetCharBytes(data);

            PrintBits(binaryData);

            Console.WriteLine();
        }

        public static bool[] ToEightBitBoolArray(this int value)
        {
            if (value < 0 || value > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 255.");
            }

            bool[] bits = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                bits[7 - i] = (value & (1 << i)) != 0;
            }

            return bits;
        }

        private static bool[] GetCharBytes(string input)
        {
            // return input.Length.ToEightBitBoolArray();

            var bytes = Encoding.ASCII.GetBytes(input);

            var result = new bool[bytes.Length * 8];

            for (int j = 0; j < bytes.Length; j++)
            {
                var bits = ((int)bytes[j]).ToEightBitBoolArray();

                for (int i = 0; i < bits.Length; i++)
                {
                    result[(j * 8) + i] = bits[i];
                }
            }

            return result;
        }

        private static void PrintBits(bool[] bits)
        {
            foreach (var bit in bits)
            {
                Console.Write(bit ? '1' : '0');
            }

            Console.WriteLine();
        }
    }
}
