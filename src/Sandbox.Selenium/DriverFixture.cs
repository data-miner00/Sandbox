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
    internal abstract class DriverFixture : IDisposable
    {
        private const double WAIT_FOR_ELEMENT_TIMEOUT = 10.0;
        private bool isDisposed;

        public DriverAdapter Driver { get; set; }
        public virtual double WaitForElementTimeout { get; set; } = WAIT_FOR_ELEMENT_TIMEOUT;

        public DriverFixture()
        {
            this.Driver = new DriverAdapter();
        }

        protected abstract void InitializeDriver();

        public void Dispose()
        {
            if (!this.isDisposed)
            {
                this.Driver.Dispose();
                this.isDisposed = true;
            }
        }
    }
}
