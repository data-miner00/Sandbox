namespace Sandbox.Images;

internal static class Grayscale
{
    public static void Transform(string filePath)
    {
        using var bitmap = new Bitmap(filePath);

        var temp = (Bitmap)bitmap.Clone();

        Color color;

        for (var i = 0; i < temp.Width; i++)
        {
            for (var j = 0; j < temp.Height; j++)
            {
                color = bitmap.GetPixel(i, j);
                byte grey = (byte)((.299 * color.R) + (.587 * color.G) + (.114 * color.B));

                temp.SetPixel(i, j, Color.FromArgb(grey, grey, grey));
            }
        }
    }
}
