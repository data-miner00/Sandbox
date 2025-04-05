namespace Sandbox.Experiment
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.KernelMemory;

    /// <summary>
    /// Reference: https://www.youtube.com/watch?v=rsW2HTM6tM8
    /// </summary>
    internal class KmTest
    {
        public static async Task IndexAndQueryServerlessDemoAsync()
        {
            var openAiKey = "foo";

            var memory = new KernelMemoryBuilder()
                .WithOpenAIDefaults(openAiKey)
                .Build<MemoryServerless>();

            await memory.ImportDocumentAsync("sample.pdf", documentId: Guid.NewGuid().ToString());

            Console.WriteLine("Ask a question about this document: ");
            var question = Console.ReadLine();

            var answer = await memory.AskAsync(question);

            Console.WriteLine($"Question: {question}\n\nAnswer: {answer.Result}");

            foreach (var source in answer.RelevantSources)
            {
                Console.WriteLine($"------------------------------");
                Console.WriteLine($"Document: {source.SourceName}");
                Console.WriteLine($"Link: {source.Link}");
                Console.WriteLine($"LastUpdated: {source.Partitions.First().LastUpdate:D}");
                Console.WriteLine($"------------------------------");
            }
        }

        /// <summary>
        /// `dotnet run setup` required
        /// </summary>
        /// <returns></returns>
        public static async Task IndexAndQueryWebServiceDemoAsync()
        {
            var memory = new MemoryWebClient("http://localhost:9001");

            var documentId = Guid.NewGuid().ToString();

            await memory.ImportDocumentAsync("sample.pdf", documentId: documentId);

            while (!await memory.IsDocumentReadyAsync(documentId))
            {
                Console.WriteLine("Waiting for document to be ready...");
                await Task.Delay(1000);
            }

            Console.WriteLine("Ask a question about this document: ");
            var question = Console.ReadLine();

            var answer = await memory.AskAsync(question);

            Console.WriteLine($"Question: {question}\n\nAnswer: {answer.Result}");

            foreach (var source in answer.RelevantSources)
            {
                Console.WriteLine($"------------------------------");
                Console.WriteLine($"Document: {source.SourceName}");
                Console.WriteLine($"Link: {source.Link}");
                Console.WriteLine($"LastUpdated: {source.Partitions.First().LastUpdate:D}");
                Console.WriteLine($"------------------------------");
            }
        }
    }
}
