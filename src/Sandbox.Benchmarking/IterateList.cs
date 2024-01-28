namespace Sandbox.Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;

    /// <summary>
    /// A class to benchmark <see cref="List{T}"/> iterations.
    /// Referenced from <see href="https://www.youtube.com/watch?v=jUZ3VKFyB-A">Nick Chapsas</see>.
    /// </summary>
    [MemoryDiagnoser]
    public class IterateList
    {
        private static readonly Random R = new(65535);

        [Params(100, 100_000, 1_000_000)]
        public int Size { get; set; }

        private List<int> items;

        [GlobalSetup]
        public void Setup()
        {
            this.items = Enumerable.Range(1, this.Size).Select(x => R.Next()).ToList();
        }

        [Benchmark]
        public void For()
        {
            for (var i = 0; i < this.items.Count; i++)
            {
                var item = this.items[i];
            }
        }

        [Benchmark]
        public void Foreach()
        {
            foreach (var item in this.items);
        }

        [Benchmark]
        public void Foreach_Linq()
        {
            this.items.ForEach(item => { });
        }

        [Benchmark]
        public void Parallel_Foreach()
        {
            Parallel.ForEach(this.items, item => { });
        }

        [Benchmark]
        public void Parallel_Foreach_Linq()
        {
            this.items.AsParallel().ForAll(item => { });
        }

        [Benchmark]
        public void For_Span()
        {
            var span = CollectionsMarshal.AsSpan(this.items);
            for (var i = 0; i < span.Length; i++)
            {
                _ = span[i];
            }
        }

        [Benchmark]
        public void Foreach_Span()
        {
            foreach (var item in CollectionsMarshal.AsSpan(this.items));
        }
    }
}
