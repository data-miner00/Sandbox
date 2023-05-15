namespace Sandbox.Concepts.Text
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    internal class RegularExpression
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
    }
}
