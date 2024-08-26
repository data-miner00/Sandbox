namespace Sandbox.Silo.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class InstantiateGenericConstructor
    {
        [Fact]
        public void Test2()
        {
            var instance1 = this.CreateGeneric<Generico>("str1", "str2");
            var instance2 = this.CreateGeneric<Maneric>("str1", "str2", 63);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
        }

        private T CreateGeneric<T>(params object[] parameters)
        {
            var obj = (T)Activator.CreateInstance(
                typeof(T), parameters);

            return obj;
        }
    }

    public abstract class IAmGeneric<T>
    {
        private readonly string field1;
        private readonly string field2;

        public T Value { get; set; }

        protected IAmGeneric(string field1, string field2)
        {
            this.field1 = field1;
            this.field2 = field2;
        }
    }

    public class Generico : IAmGeneric<string>
    {
        public Generico(string field1, string field2)
            : base(field1, field2)
        {
        }
    }

    public class Maneric : IAmGeneric<int>
    {
        private readonly int field3;

        public Maneric(string field1, string field2, int field3)
            : base(field1, field2)
        {
            this.field3 = field3;
        }
    }
}
