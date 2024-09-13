// https://stackoverflow.com/questions/1003275/how-to-convert-utf-8-byte-to-string
namespace Sandbox.Experiment;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Ext
{
    public static string ToHexString(this byte[] hex)
    {
        if (hex == null) return null;
        if (hex.Length == 0) return string.Empty;

        var s = new StringBuilder();
        foreach (byte b in hex) {
            s.Append(b.ToString("x2"));
        }
        return s.ToString();
    }

    public static byte[] ToHexBytes(this string hex)
    {
        if (hex == null) return null;
        if (hex.Length == 0) return new byte[0];

        int l = hex.Length / 2;
        var b = new byte[l];
        for (int i = 0; i < l; ++i) {
            b[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return b;
    }

    public static bool EqualsTo(this byte[] bytes, byte[] bytesToCompare)
    {
        if (bytes == null && bytesToCompare == null) return true; // ?
        if (bytes == null || bytesToCompare == null) return false;
        if (object.ReferenceEquals(bytes, bytesToCompare)) return true;

        if (bytes.Length != bytesToCompare.Length) return false;

        for (int i = 0; i < bytes.Length; ++i) {
            if (bytes[i] != bytesToCompare[i]) return false;
        }
        return true;
    }
}
