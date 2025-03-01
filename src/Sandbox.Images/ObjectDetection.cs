namespace Sandbox.Images;

using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class ObjectDetection
{
    public static void Detect(string fileName)
    {
        var image = Cv2.ImRead(fileName);
        var gray = new Mat();

        Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

        var blur = new Mat();

        Cv2.GaussianBlur(gray, blur, new OpenCvSharp.Size(5, 5), 0);

        var canny = new Mat();

        Cv2.Canny(blur, canny, 15, 120);
        Cv2.Dilate(canny, canny, new Mat(), null, 3);

        Cv2.FindContours(canny, out var contours, hierarchy: out var hierarchyIndices, mode: RetrievalModes.External, method: ContourApproximationModes.ApproxSimple);

        foreach (var contour in contours)
        {
            var rectangle = Cv2.BoundingRect(contour);
            Cv2.Rectangle(
                image,
                new OpenCvSharp.Point(rectangle.X, rectangle.Y),
                new OpenCvSharp.Point(rectangle.X + rectangle.Width, rectangle.Height + rectangle.Y),
                Scalar.DarkRed,
                2);
        }

        gray.Release();
        canny.Release();
        blur.Release();

        try
        {
            Window.ShowImages(image);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }
}
