using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Selenium
{
    internal class Utility
    {
        public Utility()
        {
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (obj, e) => Console.WriteLine(e);
            backgroundWorker.RunWorkerAsync();
        }
    }
}
