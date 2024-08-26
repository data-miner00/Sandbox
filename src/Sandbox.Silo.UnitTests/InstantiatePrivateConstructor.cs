namespace Sandbox.Silo.UnitTests;

using System.Reflection;

public class InstantiatePrivateConstructor
{
    [Fact]
    public void Test1()
    {
        SimpleClass[] classes = [
            this.CreateSimpleClass(),
            this.CreateSimpleClassV2(),
            this.CreateSimpleClass("hello"),
            this.CreateSimpleClass("bye", 55)
        ];

        Assert.Equal(classes.Length, 4);
    }

    private SimpleClass CreateSimpleClass()
    {
        var sc = (SimpleClass)typeof(SimpleClass)
            .GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                Array.Empty<Type>(),
                null)!
            .Invoke(Array.Empty<object>());

        sc.Property1 = "str";
        return sc;
    }

    private SimpleClass CreateSimpleClass(string field1)
    {
        var sc = (SimpleClass)typeof(SimpleClass)
            .GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                [typeof(string)],
                null)!
            .Invoke(new object[] { field1 });

        sc.Property1 = field1;
        return sc;
    }

    private SimpleClass CreateSimpleClass(string field1, int field2)
    {
        var sc = (SimpleClass)typeof(SimpleClass)
            .GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
                null,
                [typeof(string), typeof(int)],
                null)!
            .Invoke(new object[] { field1, field2 });

        sc.Property1 = field1;
        return sc;
    }

    private SimpleClass CreateSimpleClassV2()
    {
        var sc = (SimpleClass)Activator.CreateInstance(typeof(SimpleClass), true)!;

        sc.Property1 = "wa";
        return sc;
    }
}

public class SimpleClass
{
    private string field1;
    private int field2;

    internal SimpleClass()
        : this("f1")
    {
    }

    protected SimpleClass(string field1)
        : this(field1, 666)
    {
    }

    private SimpleClass(string field1, int field2)
    {
        this.field1 = field1;
        this.field2 = field2;
    }

    public string Property1 { get; set; }
}
