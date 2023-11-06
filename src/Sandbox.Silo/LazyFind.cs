namespace Sandbox.Silo
{
    using System;
    using System.Collections.Generic;

    public static class LazyFind
    {
        public static IEnumerable<int> Find(IEnumerable<int> list, Func<int, bool> f)
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
