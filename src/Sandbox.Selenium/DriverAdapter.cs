using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Sandbox.Selenium
{
    internal class DriverAdapter : IDisposable
    {
        private const double WAIT_FOR_ATTRIBUTE_TIMEOUT = 30;
        private IWebDriver driver;
        private WebDriverWait webDriverWait;
        private bool isDisposed;

        public DriverAdapter()
        {

        }

        public void Start(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.Latest);
                    this.driver = new FirefoxDriver();
                    break;
                case BrowserType.Safari:
                    this.driver = new SafariDriver();
                    break;
                case BrowserType.Chrome:
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.Latest);
                    this.driver = new ChromeDriver();
                    break;
                case BrowserType.Edge:
                    new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.Latest);
                    this.driver = new EdgeDriver();
                    break;
                case BrowserType.Opera:
                    new DriverManager().SetUpDriver(new OperaConfig(), VersionResolveStrategy.Latest);
                    // this.driver = new OperaDriver();
                    break;
                case BrowserType.Brave:
                    break;
            }

            this.webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(WAIT_FOR_ATTRIBUTE_TIMEOUT));

            this.driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            if (!this.isDisposed)
            {
                this.driver.Dispose();
                this.isDisposed = true;
            }
        }
    }
}
