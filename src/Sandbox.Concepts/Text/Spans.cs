namespace Sandbox.Concepts.Text
{
    using System;
    using BenchmarkDotNet.Attributes;

    [MemoryDiagnoser]
    internal class Spans
    {
        private readonly string dateAsText = "08 07 2021";

        /*
         * Ref struct can't be created on the heap.
         * private ReadOnlySpan<char> dateAsSpan = xx; wont work
         */

        /// <summary>
        /// A basic method that parse a string date to ints. However,
        /// this code is very expensive as there are a lot of heap allocated
        /// strings being constructed in the process.
        /// </summary>
        /// <returns>The parsed day, month and year.</returns>
        [Benchmark]
        public (int Day, int Month, int Year) ParseDateFromString()
        {
            var dayAsText = this.dateAsText[..2];
            var monthAsText = this.dateAsText.Substring(3, 2);
            var yearAsText = this.dateAsText[6..];
            var day = int.Parse(dayAsText);
            var month = int.Parse(monthAsText);
            var year = int.Parse(yearAsText);

            return (day, month, year);
        }

        /// <summary>
        /// The better version of the above. It optimizes by not creating
        /// new strings but utilizing the stack to store the reference
        /// of the substring such as offset and length that points directly
        /// to the original string.
        /// </summary>
        /// <returns>The parsed day, month and year.</returns>
        [Benchmark]
        public (int Day, int Month, int Year) ParseDateFromStringV2()
        {
            var dateAsSpan = (ReadOnlySpan<char>)this.dateAsText;

            var dayAsText = dateAsSpan[..2];
            var monthAsText = dateAsSpan.Slice(3, 2);
            var yearAsText = dateAsSpan[6..];
            var day = int.Parse(dayAsText);
            var month = int.Parse(monthAsText);
            var year = int.Parse(yearAsText);

            return (day, month, year);
        }
    }
}
