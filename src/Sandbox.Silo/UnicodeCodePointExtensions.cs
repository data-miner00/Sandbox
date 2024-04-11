namespace Sandbox.Silo
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Reference from <see href="https://stackoverflow.com/questions/75145712/how-can-i-tell-a-string-starts-with-an-emoji-and-get-the-first-emoji-in-the-stri">Stack overflow</see>.
    /// </summary>
    public static class UnicodeCodePointExtensions
    {
        // uses StringInfo from the System.Globalization namespace: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.stringinfo?view=net-7.0
        public static bool IsEmoji(this string inputString, int index)
        {
            return new StringInfo(inputString).IsEmoji(index);
        }

        public static bool IsEmoji(this StringInfo inputString, int index)
        {
            var firstUnicodeChar = inputString.SubstringByTextElements(index, 1); // gets the char at the given index
            var charCode = char.ConvertToUtf32(firstUnicodeChar, 0); // gets a numeric value for this char; note: we first get the char by index rather than just passing the index as an additional argument here since if there are additional utf32 chars earlier in the string our index would be offset
            return IsEmoticon(charCode)
            || IsMiscPictograph(charCode)
            || IsTransport(charCode)
            || IsMiscSymbol(charCode)
            || IsDingbat(charCode)
            || IsVariationSelector(charCode)
            || IsSupplemental(charCode)
            || IsFlag(charCode);
        }

        // these range values from https://stackoverflow.com/a/36258684/361842
        private static bool IsEmoticon(int charCode) =>
            0x1F600 <= charCode && charCode <= 0x1F64F;
        private static bool IsMiscPictograph(int charCode) =>
            0x1F680 <= charCode && charCode <= 0x1F5FF;
        private static bool IsTransport(int charCode) =>
            0x2600 <= charCode && charCode <= 0x1F6FF;
        private static bool IsMiscSymbol(int charCode) =>
            0x2700 <= charCode && charCode <= 0x26FF;
        private static bool IsDingbat(int charCode) =>
            0x2700 <= charCode && charCode <= 0x27BF;
        private static bool IsVariationSelector(int charCode) =>
            0xFE00 <= charCode && charCode <= 0xFE0F;
        private static bool IsSupplemental(int charCode) =>
            0x1F900 <= charCode && charCode <= 0x1F9FF;
        private static bool IsFlag(int charCode) =>
            0x1F1E6 <= charCode && charCode <= 0x1F1FF;
    }
}
