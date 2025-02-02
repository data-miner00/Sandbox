namespace Sandbox.Images;

internal static class Brightness
{
    public static void Transform(string filePath, short brightness = 100)
    {
        if (brightness < -255)
        {
            brightness = -255;
        }
        else if (brightness > 255)
        {
            brightness = 255;
        }

        using var bitmap = new Bitmap(filePath);

        var temp = (Bitmap)bitmap.Clone();

        Color color;

        for (var i = 0; i < temp.Width; i++)
        {
            for (var j = 0; j < temp.Height; j++)
            {
                color = bitmap.GetPixel(i, j);

                var brightR = brightness + color.R;
                var brightG = brightness + color.G;
                var brightB = brightness + color.B;

                if (brightR < 0) brightR = 1;
                if (brightR > 255) brightR = 255;
                if (brightG < 0) brightG = 1;
                if (brightG > 255) brightG = 255;
                if (brightB < 0) brightB = 1;
                if (brightB > 255) brightB = 255;

                temp.SetPixel(i, j, Color.FromArgb(brightR, brightG, brightB));
            }
        }
    }
}
