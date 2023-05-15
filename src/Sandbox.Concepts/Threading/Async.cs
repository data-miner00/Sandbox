namespace Sandbox.Concepts.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    internal class Website
    {
        public string Url { get; set; }

        public string Content { get; set; }
    }

    internal class Async
    {
        public void ExecuteSync()
        {
            var watch = Stopwatch.StartNew();

            RunCodeSync();

            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Time elapsed: {elapsedMs}");

            static void RunCodeSync()
            {
                var websites = new List<string>
                {
                    "https://www.yahoo.com",
                    "https://www.google.com",
                    "https://www.reddit.com",
                };

                foreach (var site in websites)
                {
                    Console.WriteLine(site);
                    using var client = new WebClient();
                    var website = new Website { Url = site };
                    website.Content = client.DownloadString(site);

                    Console.WriteLine($"The website is {website.Content.Length} characters long");
                }
            }
        }

        public void ExecuteAsync()
        {
            var watch = Stopwatch.StartNew();

            RunCodeAsync(); // If we did not await for an async method, the code will continue to finish the rest of the execution from main thread.

            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Time elapsed: {elapsedMs}");

            // Don't return void for a asynchronous method. `async void` can only apply for event handler.
            // Always append `Async` keyword to the async method.
            static async Task RunCodeAsync()
            {
                var websites = new List<string>
                {
                    "https://www.yahoo.com",
                    "https://www.google.com",
                    "https://www.reddit.com",
                };

                foreach (var site in websites)
                {
                    Console.WriteLine(site);
                    using var client = new WebClient();
                    var website = new Website { Url = site };
                    website.Content = await Task.Run(() => client.DownloadString(site));

                    Console.WriteLine($"The website is {website.Content.Length} characters long");
                }
            }
        }

        public async Task ExecuteAsync2()
        {
            await Task.Delay(1000);
        }

        public async Task ExecuteParallelAsync()
        {
            var watch = Stopwatch.StartNew();

            await RunCodeAsync();

            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Time elapsed: {elapsedMs}");

            static async Task RunCodeAsync()
            {
                var websites = new List<string>
                {
                    "https://www.yahoo.com",
                    "https://www.google.com",
                    "https://www.reddit.com",
                };

                var websiteTasks = new List<Task<string>>();

                foreach (var site in websites)
                {
                    Console.WriteLine(site);
                    using var client = new WebClient();
                    var website = new Website { Url = site };
                    websiteTasks.Add(Task.Run(() => client.DownloadString(site)));
                }

                var results = await Task.WhenAll(websiteTasks);

                foreach (var websiteContent in results)
                {
                    Console.WriteLine($"The website is {websiteContent.Length} characters long");
                }
            }
        }

        public async Task ExecuteNativeAsync()
        {
            using var client = new WebClient();
            await client.DownloadStringTaskAsync("https://google.com");
        }
    }
}
