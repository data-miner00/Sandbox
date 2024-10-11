namespace Sandbox.Silo;

using System;

/// <summary>
/// Whether an object is garbage collected.
/// Refer to <see href="https://stackoverflow.com/questions/15460362/how-to-tell-if-an-object-has-been-garbage-collected">this post</see>.
/// </summary>
public class GarbageCollection
{
    public sealed record Dog(string Name);

    public static void Demo()
    {
        Dog dog = new Dog("Bowser");

        WeakReference dogRef = new WeakReference(dog);
        Console.WriteLine(dogRef.IsAlive);

        dog = null;
        GC.Collect();

        Console.WriteLine(dogRef.IsAlive);
    }
}
