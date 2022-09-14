﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Selenium
{
    internal class ChromeDriverFixture : DriverFixture
    {
        protected override void InitializeDriver()
        {
            Driver.Start(BrowserType.Chrome);
        }

        public override double WaitForElementTimeout => 40.0;
    }
}
