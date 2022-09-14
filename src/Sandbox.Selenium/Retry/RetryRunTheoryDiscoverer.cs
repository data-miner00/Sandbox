using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xRetry;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Sandbox.Selenium.Retry
{
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
