namespace Sandbox.Selenium
{
    using System;
    using System.ComponentModel;

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
