namespace Sandbox.Experiment;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Hashing;

public static class Algra
{
    public static void Demo()
    {
        var checksum = Crc32.Hash(Encoding.UTF8.GetBytes("hello world"));

        Console.WriteLine(BitConverter.ToInt32(checksum));
        Console.WriteLine(Convert.ToString(BitConverter.ToInt32(checksum, 0), 2));
        Console.WriteLine(Convert.ToBase64String(checksum));
        Console.WriteLine(BitConverter.ToString(checksum));
        Console.WriteLine(Encoding.UTF8.GetString(checksum));
        Console.WriteLine(BytesToStringConverted(checksum));
    }

    static string BytesToStringConverted(byte[] bytes)
    {
        using (var stream = new MemoryStream(bytes))
        {
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
