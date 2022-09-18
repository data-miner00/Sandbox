namespace Sandbox.Selenium
{
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;

    internal class Options
    {
        public Options()
        {
            var username = Environment.GetEnvironmentVariable("USERNAME", EnvironmentVariableTarget.Machine);
            var password = Environment.GetEnvironmentVariable("Password", EnvironmentVariableTarget.Machine);
            var options = new ChromeOptions();
            options.BrowserVersion = "91.0";
            options.PlatformName = "Windows 10";
            options.AddAdditionalChromeOption("user", username);
            options.AddAdditionalChromeOption("password", password);
            options.AddAdditionalChromeOption("build", "FirstSeleniumTestsInCloud");
            options.AddAdditionalChromeOption("name", "TitleIsEqualToSamplePage_When_NavigateToHomePage");
            options.AddAdditionalChromeOption("selenium_version", "3.13.0");
            options.AddAdditionalChromeOption("console", true);
            options.AddAdditionalChromeOption("network", true);
            options.AddAdditionalChromeOption("timezone", "UTC+03:00");

            var driver = new RemoteWebDriver(new Uri(string.Empty), options);
        }
    }
}
