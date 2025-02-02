namespace Sandbox.Images;

internal static class Invert
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

                var invertR = 255 - color.R;
                var invertG = 255 - color.G;
                var invertB = 255 - color.B;

                temp.SetPixel(i, j, Color.FromArgb(invertR, invertG, invertB));
            }
        }
    }
}
