namespace Sandbox.Concepts.Events
{
    internal static class Cancel
    {
        public static void Main()
        {
            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (_, _) => cts.Cancel();
        }
    }
}
