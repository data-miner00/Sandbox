using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    internal class DangerClient
    {
        private readonly int errorRate;
        private readonly bool enableUnhandledException;
        private readonly ILogger logger;
        private int callCount = 0;

        public DangerClient(int errorRate, ILogger logger, bool enableUnhandledException)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(errorRate, 100);
            ArgumentOutOfRangeException.ThrowIfLessThan(errorRate, 0);
            ArgumentNullException.ThrowIfNull(logger);
            this.errorRate = errorRate;
            this.enableUnhandledException = enableUnhandledException;
            this.logger = logger.ForContext<DangerClient>();
        }

        public void Execute()
        {
            this.logger.Information("This method have been called {Times} times", ++this.callCount);

            var random = Random.Shared.Next(0, 100);
            if (random < this.errorRate)
            {
                if (this.enableUnhandledException && random % 2 == 0)
                {
                    throw new SystemException();
                }
                else
                {
                    throw new BarrierPostPhaseException();
                }
            }
        }
    }
}
