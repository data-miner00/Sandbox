namespace Sandbox.Silo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class LazyFind
    {
        private static IEnumerable<int> Find(IEnumerable<int> list, Func<int, bool> f)
        {
            foreach (var item in list)
            {
                if (f(item))
                {
                    yield return item;
                }
            }
        }
    }
}
