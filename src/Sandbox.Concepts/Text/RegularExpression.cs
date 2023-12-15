namespace Sandbox.Concepts.Text
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    internal static class RegularExpression
    {
        public static void SimpleMatching()
        {
            var pattern = @"(\s|^)Tim(\s|$)";
            Console.WriteLine("Tim Corey: " + Regex.IsMatch("Tim Corey", pattern));
        }

        public static void BenchmarkPrecompiled()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var pattern = @"(\s|^)Tim(\s|$)"; // compile and cached with string

            for (var i = 0; i < 100000; i++)
            {
                Regex.IsMatch("Tim", pattern);
            }

            stopwatch.Stop();

            Console.WriteLine("Elapsed: " + stopwatch.ElapsedMilliseconds);
        }

        public static void BenchmarkPrecompiledAndInstantiatedWithRegex()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var pattern = @"(\s|^)Tim(\s|$)"; // compile and cached
            var test = new Regex(pattern, RegexOptions.Compiled);

            for (var i = 0; i < 100000; i++)
            {
                test.IsMatch("Tim");
            }

            stopwatch.Stop();

            Console.WriteLine("Elapsed: " + stopwatch.ElapsedMilliseconds);
        }

        public static void BenchmarkInstantiatedWithoutCompiled()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var pattern = @"(\s|^)Tim(\s|$)";
            var test = new Regex(pattern);

            for (var i = 0; i < 100000; i++)
            {
                test.IsMatch("Tim");
            }

            stopwatch.Stop();

            Console.WriteLine("Elapsed: " + stopwatch.ElapsedMilliseconds);
        }

        public static void FileSearchWithRegex()
        {
            var contents = File.ReadAllText("index.html");
            var pattern = @"\d{3}";

            MatchCollection matches = Regex.Matches(contents, pattern);

            foreach (Match match in matches.Cast<Match>())
            {
                Console.WriteLine(match.Value);
            }
        }

        #region Newly Added
        public static void FindRepeatedWordsInString()
        {
            var regex = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var text = "The the quick   brown fox   fox jump over  the lazy lazy dog dog dog";

            var matches = regex.Matches(text);

            Console.WriteLine("{0} matches found in:\n  {1}", matches.Count, text);
        }

        public static async Task ExtractUrlsInWikipedia()
        {
            var wiki = "https://en.wikipedia.org/wiki/Linux";
            using var httpClient = new HttpClient();
            var webPageBytes = await httpClient.GetByteArrayAsync(wiki);
            using var filestream = new FileStream("linux.html", FileMode.Create);
            filestream.Write(webPageBytes, 0, webPageBytes.Length);
            filestream.Dispose();

            var readText = File.ReadAllText("linux.html");
            var pattern = "<a href=\"(.*)\" title=\"(.*)\">(.*)<\\/a>";

            var matches = Regex.Matches(readText, pattern);
            foreach (var match in matches.Cast<Match>())
            {
                var groups = match.Groups;
                Console.WriteLine($"Full match: {groups[0]}");
                Console.WriteLine($"Url: {groups[1]}, Title: {groups[2]}, InnerHtml: {groups[3]}");
            }

            Console.WriteLine("Found {0} urls.", matches.Count);
        }
        #endregion
    }
}
