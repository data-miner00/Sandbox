namespace Sandbox.Silo
{
    using System;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    /// Demonstrates ways to convert between <see cref="string"/> and <see cref="SecureString"/> back and forth.
    /// Link to <see href="https://stackoverflow.com/questions/1570422/convert-string-to-securestring">Stackoverflow post</see> and <see href="https://stackoverflow.com/questions/818704/how-to-convert-securestring-to-system-string">another one</see>.
    /// </summary>
    internal static class SecureStringDemo
    {
        public static string ToInsecureString(this SecureString secureString)
        {
            ArgumentNullException.ThrowIfNull(secureString);

            nint ptr = Marshal.SecureStringToBSTR(secureString);

            try
            {
                return Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }

        public static SecureString ToSecureString(this string str)
        {
            var secureString = new SecureString();

            foreach (char c in str)
            {
                secureString.AppendChar(c);
            }

            secureString.MakeReadOnly();

            return secureString;
        }

        public static string ToInsecureString2(this SecureString secureString)
        {
            return new NetworkCredential(string.Empty, secureString).Password;
        }

        public static SecureString ToSecureString2(this string str)
        {
            return new NetworkCredential(string.Empty, str).SecurePassword;
        }

        public static string? ToInsecureString3(this SecureString secureString)
        {
            var unmanagedString = IntPtr.Zero;

            try
            {
                // copy secure string into unmanaged memory
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                // alloc managed string and copy contents of unmanaged string data into it
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                if (unmanagedString != IntPtr.Zero)
                {
                    Marshal.FreeBSTR(unmanagedString);
                }
            }
        }

        public static unsafe SecureString ToSecureString3(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new SecureString();
            }

            fixed (char* ptr = str)
            {
                char* value = ptr;
                return new SecureString(value, str.Length);
            }
        }
    }
}
