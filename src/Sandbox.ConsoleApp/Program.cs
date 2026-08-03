namespace Sandbox.ConsoleApp
{
    using System;
    using System.Text;
    using Sandbox.Concepts.Bcl.Threading;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Metadata.Profiles.Exif;
    using SixLabors.ImageSharp.Metadata.Profiles.Iptc;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// A sandbox class to play around and experiment with.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            using Image<Rgba32> image = Image.Load<Rgba32>("IMG_3808.jpg");

            var meta = image.Metadata;

            if (meta.ExifProfile is { } exif)
            {
                exif.TryGetValue(ExifTag.Make, out var a);
                exif.TryGetValue(ExifTag.Model, out var bb);
                exif.TryGetValue(ExifTag.DateTimeOriginal, out var cc);
                DateTime.TryParse(cc.Value, out var res);
                exif.TryGetValue(ExifTag.Orientation, out var dd);
                exif.TryGetValue(ExifTag.GPSLatitude, out var gg);
                exif.TryGetValue(ExifTag.GPSLongitude, out var pp);
            }

            // IPTC — auto-populate title and tags
            if (meta.IptcProfile is { } iptc)
            {
                var ea = iptc.GetValues(IptcTag.Caption)?.FirstOrDefault()?.Value;

                var keywords = iptc.GetValues(IptcTag.Keywords)
                                   ?.Select(v => v.Value)
                                   .Where(v => !string.IsNullOrWhiteSpace(v))
                                   .ToList();
            }

            var b = image.Clone();
            b.Mutate(x => x.Invert());
            b.SaveAsJpeg("Inverted.jpeg");

            var c = image.Clone();
            c.Mutate(x => x.BlackWhite());
            c.SaveAsJpeg("BlackWhite.jpeg");

            var d = image.Clone();
            d.Mutate(x => x.Brightness(0.5f));
            d.SaveAsJpeg("Brightness.jpeg");

            var e = image.Clone();
            e.Mutate(x => x.GaussianSharpen());
            e.SaveAsJpeg("GaussianSharpen.jpeg");

            image.ProcessPixelRows(accessor =>
            {
                // Color is pixel-agnostic, but it's implicitly convertible to the Rgba32 pixel type
                Rgba32 transparent = Color.Transparent;

                for (int y = 0; y < accessor.Height; y++)
                {
                    Span<Rgba32> pixelRow = accessor.GetRowSpan(y);

                    // pixelRow.Length has the same value as accessor.Width,
                    // but using pixelRow.Length allows the JIT to optimize away bounds checks:
                    for (int x = 0; x < pixelRow.Length; x++)
                    {
                        // Get a reference to the pixel at position x
                        ref Rgba32 pixel = ref pixelRow[x];
                        if (pixel.A == 0)
                        {
                            // Overwrite the pixel referenced by 'ref Rgba32 pixel':
                            pixel = transparent;
                        }
                    }
                }
            });

            image.SaveAsJpeg("modified.jpeg");
        }
    }
}
