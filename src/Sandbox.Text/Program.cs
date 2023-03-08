/*
 * ^ - Starts with
 * $ - Ends with
 * [] - Range
 * () - Group
 * . - Single character once
 * + - one or more characters in a row
 * ? - optional preceding character
 * \ - escape character
 * \n - newline
 * \d - Digit
 * \D - non-digit
 * \s - white space
 * \S - Non white space
 * \w - alphanumeric/underscore character
 * \W - non-alphanumeric/underscore
 * {x,y} - Repeat low (x) to high(y)
 * (x|y) - Alternative - x or y
 * [^x] - Anything but x
 */

namespace Sandbox.Text
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    internal class Program
    {
        static void Main(string[] args)
        {
            var pattern = @"(\s|^)Tim(\s|$)";
            Console.WriteLine("Tim Corey: " + Regex.IsMatch("Tim Corey", pattern));

            Console.WriteLine("Hello, World!");
        }

        static void Precompiled()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var pattern = @"(\s|^)Tim(\s|$)"; // compile and cached

            for (var i = 0; i < 100000; i++)
            {
                Regex.IsMatch("Tim", pattern);
            }

            stopwatch.Stop();

            Console.WriteLine("Elapsed: " + stopwatch.ElapsedMilliseconds);
        }

        static void InstantiatedCompiled()
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

        static void InstantiatedNotCompiled()
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

        static void FileSearch()
        {
            var contents = File.ReadAllText(".txt");
            var pattern = @"\d{3}";

            MatchCollection matches = Regex.Matches(contents, pattern);

            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
    }
}
