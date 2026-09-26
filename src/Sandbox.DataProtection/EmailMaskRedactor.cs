namespace Sandbox.DataProtection;

using Microsoft.Extensions.Compliance.Redaction;

/// <summary>
/// Masks the local part of an email address, keeping only its first few characters
/// followed by a fixed number of mask characters, while leaving the domain intact.
/// e.g. <c>john.doe@example.com</c> becomes <c>joh*****@example.com</c>.
/// </summary>
public class EmailMaskRedactor : Redactor
{
    private const int VisibleCharCount = 3;
    private const int MaskCharCount = 5;
    private const char MaskChar = '*';

    public override int GetRedactedLength(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return 0;
        }

        Split(input, out var visible, out var domain);
        return visible.Length + MaskCharCount + domain.Length;
    }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        if (source.IsEmpty)
        {
            return 0;
        }

        Split(source, out var visible, out var domain);

        visible.CopyTo(destination);
        var written = visible.Length;

        destination.Slice(written, MaskCharCount).Fill(MaskChar);
        written += MaskCharCount;

        domain.CopyTo(destination[written..]);
        written += domain.Length;

        return written;
    }

    /// <summary>
    /// Splits the input into the visible prefix of the local part and the domain (including the '@').
    /// Input without an '@' is treated as a local part with no domain.
    /// </summary>
    private static void Split(ReadOnlySpan<char> input, out ReadOnlySpan<char> visible, out ReadOnlySpan<char> domain)
    {
        // Use the last '@' since a quoted local part may legally contain '@'.
        var at = input.LastIndexOf('@');
        var local = at >= 0 ? input[..at] : input;
        domain = at >= 0 ? input[at..] : ReadOnlySpan<char>.Empty;
        visible = local[..Math.Min(VisibleCharCount, local.Length)];
    }
}
