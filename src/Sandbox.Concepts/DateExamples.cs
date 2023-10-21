namespace Sandbox.Concepts
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Demonstrating some of the techniques when dealing with DateTime.
    /// </summary>
    internal static class DateExamples
    {
        public static double DifferenceBetweenTwoDate(DateTime date1, DateTime date2)
        {
            return (date1 - date2).TotalDays;
        }

        public static int DifferenceBetweenTwoDateInt(DateTime date1, DateTime date2)
        {
            return date1.Subtract(date2).Days;
        }

        public static void UsingCultureInfo()
        {
            var localDate = DateTime.Now;
            var newMonth = localDate.Month + 3;
            var culture = new CultureInfo("en-US");

            Console.WriteLine(localDate.ToString(culture));
            Console.WriteLine(localDate.ToString("M/d/yyyy"));
            Console.WriteLine($"Month index: {newMonth}");
        }
    }
}
