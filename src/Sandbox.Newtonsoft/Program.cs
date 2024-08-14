namespace Sandbox.Newtonsoft;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::Newtonsoft.Json;
using global::Newtonsoft.Json.Linq;

internal class Program
{
    public static void Main(string[] args)
    {
        var rawJson = File.ReadAllTextAsync("unsanitized.json").GetAwaiter().GetResult();

        var processed = JsonProcessor.ProcessJson(rawJson);

        var obj = JsonConvert.DeserializeObject(processed);

        Console.ReadKey();
    }
}
