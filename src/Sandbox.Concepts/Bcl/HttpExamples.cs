namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Threading.Tasks;

    public static class HttpExamples
    {
        public static async Task Example()
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync("https://google.com");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                await Console.Out.WriteLineAsync(responseBody);
            }
            catch (HttpRequestException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
