namespace Sandbox.Silo;

internal sealed class Factory<K, T>
    where T : class, K, new()
{
    public static K GetInstance()
    {
        K obj;
        obj = new T();
        return obj;
    }
}
