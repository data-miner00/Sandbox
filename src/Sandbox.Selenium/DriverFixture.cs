namespace Sandbox.Selenium
{
    using System;
    using System.Threading;

    internal abstract class DriverFixture : IDisposable
    {
        private const double WAIT_FOR_ELEMENT_TIMEOUT = 10.0;
        private bool isDisposed;

        public ThreadLocal<DriverAdapter> Driver { get; set; }

        public static ThreadLocal<string>? TestName { get; set; }

        public virtual double WaitForElementTimeout { get; set; } = WAIT_FOR_ELEMENT_TIMEOUT;

        public DriverFixture()
        {
#if DEBUG
            this.Driver = new ThreadLocal<DriverAdapter>(() => new DriverAdapter());
#elif RELEASE
            this.Driver = new ThreadLocal<DriverAdapter>(() => new DriverAdapter());
#else
            throw new ArgumentException("Test environment not supported");
#endif
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
