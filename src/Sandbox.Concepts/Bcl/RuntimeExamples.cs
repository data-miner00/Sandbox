namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Runtime.InteropServices;

    public static class RuntimeExamples
    {
        [DllImport("some.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(nint hWnd, String text, String caption, uint type);

        public static void Example()
        {
            _ = MessageBox(new IntPtr(0), "Hello World!", "Hello hello", 0);
        }
    }
}
