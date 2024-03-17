namespace Sandbox.Concepts.Text
{
    using System;
    using System.IO.Pipes;
    using System.Runtime.InteropServices;

    public partial class StringOptimizations
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

    /// <summary>
    /// An example at <see href="https://youtu.be/3r6gbZFRDHs?si=-C7_uiC-Y25IR3cc&t=1163">.NET Fest 2019</see>.
    /// </summary>
    public partial class StringOptimizations
    {
        public static unsafe void ZeroCopySlicing()
        {
            var array = new int[64];
            var span1 = new Span<int>(array);
            var span2 = new Span<int>(array, start: 8, length: 4);

            Span<int> span3 = stackalloc[] { 1, 2, 3, 4, 5 };

            IntPtr memory = Marshal.AllocHGlobal(64);
            void* ptr = memory.ToPointer();
            var span4 = new Span<byte>(ptr, 64);

            var str = "Hello world";
            ReadOnlySpan<char> span5 = str.AsSpan();

            // Zero-copy slicing
            var span6 = span1[..4];
        }

        public static async Task DoAsync(Memory<char> data)
        {
            DoSync(data.Span[..10]);
            await Console.Out.WriteLineAsync("Doing other async tasks");
        }

        public static void DoSync(Span<char> data)
        {
            Console.WriteLine("The length is: {0}", data.Length);
        }
    }
}
