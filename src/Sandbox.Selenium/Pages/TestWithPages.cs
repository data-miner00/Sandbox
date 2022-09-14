namespace Sandbox.Selenium.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;
    using WebDriverManager.Helpers;

    public class TestWithPages
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait webDriverWait;
        private readonly Actions actions;
        private readonly MainPage mainPage;

        public TestWithPages()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            this.driver = new ChromeDriver();
            this.webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
            this.actions = new Actions(this.driver);
            this.mainPage = new MainPage(this.driver, this.webDriverWait, this.actions);
        }

        [Fact]
        public void VerifyTodo()
        {
            this.mainPage.AddNewToDoItem(string.Empty);
            // test codes
        }
    }
}
