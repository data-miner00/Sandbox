using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    internal class DangerClient
    {
        private readonly int errorRate;
        private readonly ILogger logger;
        private int callCount = 0;

        public DangerClient(int errorRate, ILogger logger)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(errorRate, 100);
            ArgumentOutOfRangeException.ThrowIfLessThan(errorRate, 0);
            ArgumentNullException.ThrowIfNull(logger);
            this.errorRate = errorRate;
            this.logger = logger.ForContext<DangerClient>();
        }

        public void Execute()
        {
            this.logger.Information("This method have been called {Times} times", ++this.callCount);

            if (Random.Shared.Next(0, 100) < this.errorRate)
            {
                throw new BarrierPostPhaseException();
            }
        }
    }
}
