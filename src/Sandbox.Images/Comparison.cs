namespace Sandbox.Images;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

internal static class Comparison
{
    public static void Compare(string file1, string file2)
    {
        var output = new Mat();

        var image1 = new Mat(file1);
        image1.ConvertTo(image1, MatType.CV_32FC1);

        var image2 = new Mat(file2);
        image2.ConvertTo(image2, MatType.CV_32FC2);

        Cv2.MatchTemplate(image1, image2, output, TemplateMatchModes.CCoeffNormed);
        Cv2.MinMaxLoc(output, out double res1, out var res2);

        Console.WriteLine("Matching index: " + res2); // 1 means exact match
    }
}
