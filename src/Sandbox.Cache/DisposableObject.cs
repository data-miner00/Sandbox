namespace Sandbox.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class DisposableObject : IDisposable
    {
        private string id = Guid.NewGuid().ToString();
        private bool isDisposed;
        private object _lock = new object();
        private int _count = 0;

        public int Count => _count;

        public void Execute()
        {
            lock (_lock)
            {
                _count++;
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose method called. " + id);

            if (!isDisposed)
            {
                isDisposed = true;

                Console.WriteLine($"{id} object disposed.");
            }
        }
    }
}
