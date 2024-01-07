namespace Sandbox.Benchmarking;

using BenchmarkDotNet.Attributes;

[MemoryDiagnoser(false)]
public class SumOdd
{
    private readonly int[] array = Enumerable.Range(1, 10_000).ToArray();

    [Benchmark]
    public int SumOdd_Normal()
    {
        var counter = 0;
        for (var i = 0; i < array.Length; i++)
        {
            var element = array[i];
            if (element % 2 == 0)
            {
                counter += element;
            }
        }

        return counter;
    }

    [Benchmark]
    public int SumOdd_FilterReduce()
    {
        return array.Where(x => x % 2 == 0).Sum();
    }

    [Benchmark]
    public int SumOdd_BitManip()
    {
        var counter = 0;
        for (int i = 0; i < array.Length; ++i)
        {
            var element = array[i];
            if ((element & 1) == 1)
            {
                counter += element;
            }
        }

        return counter;
    }

    [Benchmark]
    public int SumOdd_Branchless_BitManip()
    {
        var counter = 0;
        for (var i = 0; i < array.Length; i++)
        {
            var element = array[i];
            var odd = element & 1;
            counter += (odd * element);
        }

        return counter;
    }

    [Benchmark]
    public int SumOdd_Branchless_BitManip_Parallel()
    {
        int counterA = 0, counterB = 0;

        for (var i = 0; i < array.Length; i += 2)
        {
            var elementA = array[i];
            var elementB = array[i + 1];

            var oddA = elementA & 1;
            var oddB = elementB & 1;

            counterA += (oddA * elementA);
            counterB += (oddB * elementB);
        }

        return counterA + counterB;
    }

    [Benchmark]
    public int SumOdd_Branchless_BitManip_Parallel_NoMul()
    {
        int counterA = 0, counterB = 0;

        for (var i = 0; i < array.Length; i += 2)
        {
            var elementA = array[i];
            var elementB = array[i + 1];

            counterA += (elementA << (elementA & 1)) - elementA;
            counterB += (elementB << (elementB & 1)) - elementB;
        }

        return counterA + counterB;
    }

    [Benchmark]
    unsafe public int SumOdd_Branchless_BitManip_Parallel_Boundless()
    {
        int counterA = 0, counterB = 0;

        fixed (int* data = &array[0])
        {
            var p = (int*)data;

            for (var i = 0; i < array.Length; i += 2)
            {
                counterA += (p[0] & 1) * p[0];
                counterB += (p[1] & 1) * p[1];

                p += 2;
            }
        }

        return counterA + counterB;
    }

    [Benchmark]
    unsafe public int SumOdd_Branchless_BitManip_Parallel_Boundless_QuadCore()
    {
        int counterA = 0, counterB = 0, counterC = 0, counterD = 0;

        fixed (int* data = &array[0])
        {
            var p = (int*)(data);

            for (var i = 0; i < array.Length; i += 4)
            {
                counterA += (p[0] & 1) * p[0];
                counterB += (p[1] & 1) * p[1];
                counterC += (p[2] & 1) * p[2];
                counterD += (p[3] & 1) * p[3];

                p += 4;
            }
        }

        return counterA + counterB + counterC + counterD;
    }

    [Benchmark]
    unsafe public int SumOdd_Branchless_BitManip_Parallel_Boundless_QuadCore_BetterPorts()
    {
        int counterA = 0, counterB = 0, counterC = 0, counterD = 0;

        fixed (int* data = &array[0])
        {
            var p = (int*)(data);
            var n = (int*)(data);

            for (var i = 0; i < array.Length; i += 4)
            {
                counterA += (n[0] & 1) * p[0];
                counterB += (n[1] & 1) * p[1];
                counterC += (n[2] & 1) * p[2];
                counterD += (n[3] & 1) * p[3];

                p += 4;
                n += 4;
            }
        }

        return counterA + counterB + counterC + counterD;
    }
}
