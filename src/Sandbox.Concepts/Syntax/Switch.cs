namespace Sandbox.Concepts.Syntax
{
    using System;

    /// <summary>
    /// Switch statement that has the cpability to check for conditions.
    /// </summary>
    internal static class Switch
    {
        public static void Demo()
        {
            switch (10)
            {
                case var d when d >= 10:
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }
    }
}
