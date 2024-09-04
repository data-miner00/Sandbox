namespace Sandbox.Experiment;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Hashing;

public class Algra
{
    public static void Demo()
    {
        var crc32 = new Crc32();

        crc32.Append(Encoding.ASCII.GetBytes("Hello world"));

        Console.WriteLine(crc32.GetCurrentHash());
    }
}
