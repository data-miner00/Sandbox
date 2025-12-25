namespace Sandbox.Silo;

public static class IEnumerableExtensions
{
    public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out IEnumerable<T> rest)
    {
        first = seq.FirstOrDefault();
        rest = seq.Skip(1);
    }

    public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out IEnumerable<T> rest)
        => (first, (second, rest)) = seq;

    public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out IEnumerable<T> rest)
        => (first, second, (third, rest)) = seq;

    public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth, out IEnumerable<T> rest)
        => (first, second, third, (fourth, rest)) = seq;

    public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth, out T fifth, out IEnumerable<T> rest)
        => (first, second, third, fourth, (fifth, rest)) = seq;

    private static void Demo()
    {
        var nums = new List<int> { 1, 34, 32, 6, 989, 7, 24, 0 };

        // Simple deconstruction (first element + remaining sequence)
        var (first, rest) = nums;
        // first == 1, rest => {34, 32, 6, 989, 7, 24, 0}

        // Deconstruct first two elements + remaining sequence
        var (f1, f2, rest2) = nums;
        // f1 == 1, f2 == 34, rest2 => {32, 6, 989, 7, 24, 0}

        // Deconstruct first three elements + remaining sequence
        var (a, b, c, rest3) = nums;
        // a == 1, b == 34, c == 32, rest3 => {6, 989, 7, 24, 0}

        // You can discard values you don't need
        var (head, _, _, tail) = nums;
        // head == 1, tail => {6, 989, 7, 24, 0}    }
    }
}
