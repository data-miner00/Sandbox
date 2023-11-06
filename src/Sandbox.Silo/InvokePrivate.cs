namespace Sandbox.Silo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class InvokePrivate
    {
        private static MethodInfo? CachedGetSecret = typeof(SomeClass)
            .GetMethod("GetSecret", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void Invoke()
        {
            var some = new SomeClass();

            var secret = typeof(SomeClass)
                .GetMethod("GetSecret", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)?
                .Invoke(some, Array.Empty<object>());

            var field = typeof(SomeClass)
                .GetField("field", BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(some);

            var property = typeof(SomeClass)
                .GetProperty("get_Property", BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(some);

            typeof(SomeClass)
                .GetProperty("set_Property", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(some, "new name");
        }
    }

    public sealed class SomeClass
    {
        private readonly string field = "this secret too";

        private string Property { get; set; } = "hello";

        private string GetSecret()
        {
            return "this is secret";
        }
    }
}
