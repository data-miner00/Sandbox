using System.Text;

namespace Benchmarks.Core.Net10;

public static class JoinArray
{
    public static string JoinArray1<T>(T[] array)
    {
        var str = string.Empty;
        foreach (var item in array)
        {
            str += item.ToString();
        }

        return str;
    }

    public static string JoinArray2<T>(T[] array)
    {
        var sb = new StringBuilder();
        foreach (var item in array)
        {
            sb.Append(item.ToString());
        }

        return sb.ToString();
    }

    public static string JoinArray3<T>(T[] array)
    {
        return string.Join("", array);
    }
}
