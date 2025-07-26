namespace Sandbox.Selenium
{
    internal class ChromeDriverFixture : DriverFixture
    {
        protected override void InitializeDriver()
        {
            Driver.Value?.Start(BrowserType.Chrome);
        }

        public override double WaitForElementTimeout => 40.0;
    }
}
