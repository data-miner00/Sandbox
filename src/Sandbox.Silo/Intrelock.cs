namespace Sandbox.Silo;

/// <summary>
/// Thread-safe, atomic operations.
/// </summary>
internal class Intrelock
{
    private long value = 0;

    public long Value => Interlocked.Read(ref this.value);

    public void Increment()
    {
        Interlocked.Increment(ref this.value);
    }

    public void Add(long number)
    {
        Interlocked.Add(ref this.value, number);
    }
}
