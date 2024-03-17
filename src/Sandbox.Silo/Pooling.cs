namespace Sandbox.Silo
{
    using System;
    using System.Buffers;

    internal static class Pooling
    {
        public static void Demo(int minLength)
        {
            var pool = ArrayPool<int>.Shared;
            int[] buffer = pool.Rent(minLength);

            try
            {
                Console.WriteLine(buffer);
            }
            finally
            {
                pool.Return(buffer);
            }
        }
    }
}
