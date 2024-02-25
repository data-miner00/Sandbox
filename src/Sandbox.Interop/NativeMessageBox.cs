namespace Sandbox.Interop;

using System.Runtime.InteropServices;

/// <summary>
/// A small class to test out the native MessageBox alert.
/// </summary>
internal static class NativeMessageBox
{
    /// <summary>
    /// Displays the message in a native Windows alert box.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The content to be displayed.</param>
    public static void DisplayMessage(string title, string message)
    {
        _ = MessageBox((nint)0, message, title, 0);
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int MessageBox(nint hwnd, string message, string title, int flag); // nint is equivalent to IntPtr struct
}
