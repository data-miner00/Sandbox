namespace Sandbox.Concepts.Bcl
{
    using System;

    public static class UriExamples
    {
        public static void Examples()
        {
            var uri = new Uri("https://simple.uri");

            if (uri.Scheme is not null)
            {
                Console.WriteLine("Scheme " + uri.Scheme);
            }

            if (uri.Authority is not null)
            {
                Console.WriteLine("Authority " + uri.Authority);
            }

            if (uri.PathAndQuery is not null)
            {
                Console.WriteLine("PathAndQuery " + uri.PathAndQuery);
            }

            if (uri.Fragment is not null)
            {
                Console.WriteLine("Fragment " + uri.Fragment);
            }
        }

        public static void UsingUriBuilder()
        {
            var builder = new UriBuilder
            {
                Scheme = "http",
                Host = "localhost",
                Port = 8080,
                Path = "/blogs",
                Query = "tag=csharp&tag=dotnet",
            };

            var uri = builder.Uri;
            Console.WriteLine(uri.ToString());
        }
    }
}
