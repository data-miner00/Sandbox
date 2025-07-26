namespace Sandbox.Selenium.Retry
{
    using System.ComponentModel;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    [Serializable]
    internal class RetryRunTestCase : XunitTestCase
    {
        private int maxRetries;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public RetryRunTestCase()
        {
        }

        public RetryRunTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay testMethodDisplay, TestMethodDisplayOptions defaultMedhodDisplayOptions, ITestMethod testMethod, int maxRetries)
            : base(diagnosticMessageSink, testMethodDisplay, defaultMedhodDisplayOptions, testMethod, testMethodArguments: null)
        {
            this.maxRetries = maxRetries;
        }

        public override async Task<RunSummary> RunAsync(
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            object[] constructorArguments,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
        {
            var runCount = 0;
            RunSummary summary;

            while (true)
            {
                var delayedMessageBus = new DelayedMessageBus(messageBus);
                summary = await base.RunAsync(diagnosticMessageSink, delayedMessageBus, constructorArguments, aggregator, cancellationTokenSource);
                diagnosticMessageSink.OnMessage(new DiagnosticMessage("Execution of '{0}' failed (attempt #{1}), retrying...", DisplayName, ++runCount));

                if (runCount == maxRetries)
                {
                    break;
                }
            }

            return summary;
        }

        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);
        }

        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);
        }
    }
}
