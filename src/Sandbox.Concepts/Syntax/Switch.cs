namespace Sandbox.Concepts.Syntax;

using System;

/// <summary>
/// Switch statement that has the cpability to check for conditions.
/// </summary>
internal static class Switch
{
    public static void Demo()
    {
        switch (10)
        {
            case var d when d >= 10:
                Console.WriteLine();
                break;
            default:
                Console.WriteLine();
                break;
        }
    }

    public static void GotoSwitch()
    {
        switch (Levels.Level1)
        {
            case Levels.Level1:
                Console.WriteLine("Level 1");
                goto case Levels.Level2;
            case Levels.Level2:
                Console.WriteLine("Level 2");
                goto case Levels.Level3;
            case Levels.Level3:
                Console.WriteLine("Level 3");
                goto case Levels.Level4;
            case Levels.Level4:
                Console.WriteLine("Level 4");
                goto case Levels.Level5;
            case Levels.Level5:
                Console.WriteLine("Level 5");
                goto case Levels.Level6;
            case Levels.Level6:
                Console.WriteLine("Level 6");
                goto case Levels.Level7;
            case Levels.Level7:
                Console.WriteLine("Level 7");
                goto case Levels.Level8;
            case Levels.Level8:
                Console.WriteLine("Level 8");
                goto case Levels.Level1;
            default:
                Console.WriteLine("Ended");
        }
    }
}

file enum Levels
{
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8,
}
