using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;

namespace Sandbox.Selenium
{
    internal class DriverFixture : IDisposable
    {
        private const double WAIT_FOR_ELEMENT_TIMEOUT = 10.0;
        private bool isDisposed;

        public WebDriverWait WebDriverWait { get; set; }
        public IWebDriver WebDriver { get; set; }

        

        public DriverFixture()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.Latest);
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");

            this.WebDriver = new ChromeDriver(chromeOptions);
            this.WebDriverWait = new WebDriverWait(this.WebDriver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
        }

        public void Dispose()
        {
            if (!this.isDisposed)
            {

                this.isDisposed = true;
            }
        }
    }
}
