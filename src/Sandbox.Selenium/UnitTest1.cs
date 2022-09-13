namespace Sandbox.Selenium
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;
    using WebDriverManager.Helpers;
    using xRetry;

    public class UnitTest1 : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait webDriverWait;
        private bool isDisposed;

        public double WAIT_FOR_ELEMENT_TIMEOUT { get; } = 10.0;

        public UnitTest1()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");

            this.driver = new ChromeDriver(chromeOptions);
            this.webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
        }

        [RetryFact]
        [Trait("Category", "CI")]
        [Trait("Priority", "1")]
        public void QueryElement_SendKeysToInput()
        {
            var todoInput = this.driver.FindElement(By.Id("my_id"));
            var birthday = new DateTime(1990, 10, 20);
            todoInput.SendKeys(birthday.ToString("d"));
        }

        [Fact]
        public void QueryByXPath_Click_Last()
        {
            var todoCheckBoxes = this.driver.FindElements(By.XPath("//[li[@ng-repeat]/input"));
            todoCheckBoxes.Last().Click();

            var todoInfos = driver.FindElements(By.XPath("//li[@ng-repeat]/span/preceding-sibling::input"));

            todoInfos.Last().Text.Equals("10/20/1990");
        }

        [Fact]
        public void UseWaitAndFind()
        {
            var element = this.WaitAndFindElement(By.LinkText("Anchor"));

        }

        private IWebElement WaitAndFindElement(By locator)
        {
            return this.webDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                this.driver.Quit();
                isDisposed = true;
            }
        }
    }
}