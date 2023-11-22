namespace Sandbox.Silo
{
    using System;

    internal static class ResourceConsumer
    {
        public static void Consume()
        {
            Console.WriteLine(Resources.StringWithinResource);
        }
    }
}
