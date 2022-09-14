namespace Sandbox.Selenium.Retry
{
    using Xunit.Sdk;

    /// <summary>
    /// Works like [Fact] but failures are retried (by default 3 times).
    /// </summary>
    [XunitTestCaseDiscoverer("RetryFactDiscoverer", "Sandbox.Selenium")]
    internal class RetryFactAttribute : FactAttribute
    {
        /// <summary>
        /// Number if retries allowed for a failed test.
        /// </summary>
        public int MaxRetries { get; set; }
    }
}
