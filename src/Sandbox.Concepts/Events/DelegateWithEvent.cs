namespace Sandbox.Concepts.Events
{
    using System;

    /// <summary>
    /// An arbitrary delegate declared at the namespace level.
    /// </summary>
    /// <param name="sender">The object that emits the event.</param>
    /// <param name="args">The arguments passed along with the event.</param>
    internal delegate void EventDelegate(object? sender, EventArgs args);

    /// <summary>
    /// A sample class that demonstrates working with delegate with events.
    /// </summary>
    internal static class DelegateWithEvent
    {
        private static int counter = 0;

        private delegate string DelegateFunc(string arg);

        private static event EventDelegate? EventDelegate;

        /// <summary>
        /// Examples of working with delegates.
        /// </summary>
        public static void InstantiatingDelegates()
        {
            // Instantiating delegate
            DelegateFunc func = new(DelegateFuncImpl);

            // Syntatic sugar for instantiating delegate
            DelegateFunc _ = DelegateFuncImpl;

            // Chain with second implementation as multicast delegate
            func += DelegateFuncImpl2;

            // Calling the delegate implementations
            var res = func("No matter what I passed it doesn't matter");

            // Display result
            Console.WriteLine($"Result: {res}"); // should be `const`
            Console.WriteLine($"Counter: {counter}"); // should be `1`
        }

        /// <summary>
        /// Checking for subscribers.
        /// </summary>
        public static void CheckForSubscribersBeforeInstantiating()
        {
            EventDelegate?.Invoke(null, new EventArgs());

            EventDelegate += OnEventRaised;
            EventDelegate = null;
        }

        private static string DelegateFuncImpl(string s) => s;

        private static string DelegateFuncImpl2(string _)
        {
            Console.WriteLine($"The params is passed equally to all funcs: {_}");
            counter++;
            return "const";
        }

        private static void OnEventRaised(object? sender, EventArgs args)
        {
            Console.WriteLine(args);
        }
    }
}
