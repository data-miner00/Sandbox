namespace Sandbox.Selenium.Retry
{
    using System.Collections.Generic;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    internal class RetryRunTheoryDiscoverer : IXunitTestCaseDiscoverer
    {
        internal readonly IMessageSink diagnosticMessageSink;

        public RetryRunTheoryDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var maxRetries = factAttribute.GetNamedArgument<int>("MaxRetries");
            if (maxRetries < 1)
            {
                maxRetries = 3;
            }

            yield return new RetryRunTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod, maxRetries);
        }
    }
}
