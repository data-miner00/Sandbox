namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Globalization;

    public static class GlobalizationExamples
    {
        public static void Example()
        {
            var cultureInfo = new CultureInfo("zh-CN");
            CultureInfo.CurrentCulture = cultureInfo;

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Console.WriteLine("{0:C2}", 1234.56);

            // Date
            Console.WriteLine(DateTime.Now.ToString(cultureInfo));
        }

        public static void SpecificCulture()
        {
            var cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;

            // Calendar - Default
            Console.WriteLine(cultureInfo.DateTimeFormat.Calendar);
            Console.WriteLine(DateTime.Now.ToString("d"));

            // Calendar - Taiwan
            cultureInfo.DateTimeFormat.Calendar = new TaiwanCalendar();
            Console.WriteLine(cultureInfo.DateTimeFormat.Calendar);
            Console.WriteLine(DateTime.Now.ToString("d"));
        }

        public static void DateTimeDemo()
        {
            CultureInfo[] cultures = [
                new("en-Us"),
                new("fr-FR"),
                new("de-DE")
            ];

            foreach (var culture in cultures)
            {
                Console.WriteLine(culture.Name, DateTime.Now.ToString(culture));
            }
        }

        public static void CurrencyDemo()
        {
            var money = 1234.56m;

            CultureInfo[] cultures = [
                new("fr-CA"),
                new("en-USA"),
                new("ms-MY"),
            ];

            foreach (var culture in cultures)
            {
                CultureInfo.CurrentCulture = culture;
                Console.WriteLine("{0}: {1:C2}", CultureInfo.CurrentCulture.Name, money);
            }
        }
    }
}
