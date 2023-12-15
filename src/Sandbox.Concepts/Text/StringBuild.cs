namespace Sandbox.Concepts.Text
{
    using System;
    using System.Diagnostics;
    using System.Text;

    internal static class StringBuild
    {
        public static void SBExample()
        {
            var numberWords = new[] { 100, 10_000, 100_000 };

            var phrase = File.ReadAllText("lorem.txt");
            var words_total = phrase.Split(' ');

            foreach (var size in numberWords)
            {
                var words = new string[size];

                Array.Copy(words_total, words, words.Length);

                var loremString = string.Empty;
                var loremStringBuilder = new StringBuilder();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                foreach (var word in words)
                {
                    loremString += word;
                }

                stopwatch.Stop();
                var timeElapsed = stopwatch.Elapsed;

                Console.WriteLine("{0} with {1} elements", timeElapsed.TotalMilliseconds, size);
                stopwatch.Reset();

                stopwatch.Start();
                foreach (var word in words)
                {
                    loremStringBuilder.Append(word);
                }

                _ = loremStringBuilder.ToString();

                stopwatch.Stop();
                timeElapsed = stopwatch.Elapsed;

                Console.WriteLine("{0} with {1} elements", timeElapsed.TotalMilliseconds, size);
                stopwatch.Reset();
            }
        }
    }
}
