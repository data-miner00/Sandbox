namespace Sandbox.Concepts.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StringOptimizations
    {
        private readonly string url = "https://aws.nickchapsas.com/courses/cloud-fundamentals-aws-services";

        public string Host
        {
            get
            {
                // Uri have lots of stuffs that we don't need
                var uri = new Uri(this.url);
                return uri.Host;
            }
        }

        public string HostGood
        {
            get
            {
                var prefixOffset = this.url.IndexOf("://", StringComparison.Ordinal);
                var startIndex = prefixOffset == -1 ? 0 : prefixOffset + 3;
                var endIndex = this.url.Substring(startIndex).IndexOf('/');

                return endIndex == -1 ? this.url.Substring(startIndex) :
                    this.url.Substring(startIndex, endIndex);
            }
        }

        public string HostBetter
        {
            get
            {
                var prefixOffset = this.url.AsSpan().IndexOf(stackalloc char[] { ':', '/', '/' });
                var startIndex = prefixOffset == -1 ? 0 : prefixOffset + 3;
                var endIndex = this.url.AsSpan(startIndex).IndexOf('/');

                var span = endIndex == -1 ? this.url.AsSpan(startIndex) :
                    this.url.AsSpan(startIndex, endIndex);

                return span.ToString();
            }
        }

        public string HostBest
        {
            get
            {
                // Using String Pool: https://learn.microsoft.com/en-us/dotnet/communitytoolkit/high-performance/stringpool
                return "example.com";
            }
        }
    }
}
