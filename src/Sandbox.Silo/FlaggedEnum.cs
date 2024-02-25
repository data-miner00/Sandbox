namespace Sandbox.Silo
{
    using System;
    using System.Runtime.InteropServices;

    [Flags]
    internal enum FlaggedEnum : int
    {
        FirstValue = 1 << 0,
        SecondValue = 1 << 1,
        ThirdValue = 1 << 2,
        FourthValue = 1 << 3,
        FifthValue = 1 << 4,
        SixthValue = 0x10,
    }
}
