namespace Sandbox.Selenium.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// The main page of testing.
    /// </summary>
    internal class MainPage
    {
        private IWebDriver driver;
        private WebDriverWait webDriverWait;
        private Actions actions;

        public MainPage(IWebDriver driver, WebDriverWait webDriverWait, Actions actions)
        {
            this.driver = driver;
            this.webDriverWait = webDriverWait;
            this.actions = actions;
        }

        // Elements (can seperate these section into partial class)
        public IWebElement ToDoInput => WaitAndFindElement(By.XPath("//"));

        public IWebElement GetItemCheckBox(string todoItem)
        {
            return WaitAndFindElement(By.XPath("//"));
        }

        // Actions
        public void AddNewToDoItem(string todoItem)
        {
            ToDoInput.SendKeys(todoItem);
            this.actions.Click(ToDoInput).SendKeys(Keys.Enter).Perform();
        }

        public void CheckItem(string itemName)
        {
            GetItemCheckBox(itemName).Click();
        }

        // Assertions
        public void AssertLeftItems(int expectedCount)
        {
            var resultSpan = WaitAndFindElement(By.XPath("//"));
            if (expectedCount <= 0)
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
            else
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
        }

        // Private helpers
        public void ValidateInnerTextIs(IWebElement resultSpan, string expectedText)
        {
            this.webDriverWait.Until(ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText));
        }

        private IWebElement WaitAndFindElement(By selector)
        {
            return this.webDriverWait.Until(ExpectedConditions.ElementExists(selector));
        }
    }
}
