namespace Sandbox.Selenium
{
    internal class FirefoxDriverFixture : DriverFixture
    {
        protected override void InitializeDriver()
        {
            Driver.Value?.Start(BrowserType.Firefox);
        }

        public override double WaitForElementTimeout => 40.0;
    }
}
