using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Azure.EntraID.Options
{
    internal class AppCredentials
    {
        public const string SectionName = "appCredentials";

        public string TenantId { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
