namespace Sandbox.Events
{
    using System;

    internal delegate void EventDelegate(object? sender, EventArgs args);

    internal static class Delegates
    {
        public static int counter = 0;

        public delegate string DelegateFunc(string arg);

        public static void Demo()
        {
            DelegateFunc func = new DelegateFunc(DelegateFuncImpl); // instantiate the delegate and point to DelegateFuncImpl

            DelegateFunc _ = DelegateFuncImpl; // syntatic sugar that does the above

            func += DelegateFuncImpl2; // multicast delegate

            var res = func("rand string");

            Console.WriteLine($"Result: {res}");
            Console.WriteLine($"Counter: {counter}");
        }

        private static string DelegateFuncImpl(string s)
        {
            return s;
        }

        private static string DelegateFuncImpl2(string _)
        {
            Console.WriteLine($"The params is passed equally to all funcs: {_}");
            counter++;
            return "const";
        }

        public static void SafeDemo()
        {
            if (EventDelegate != null) // check if anyone is listening to the event, if not, no need to call it
            {
                EventDelegate(null, new EventArgs());
            }

            EventDelegate += OnEventRaised;

            // EventDelegate = null;
        }

        public static event EventDelegate EventDelegate;

        public static void OnEventRaised(object? sender, EventArgs args)
        {
            Console.WriteLine(args);
        }
    }
}
