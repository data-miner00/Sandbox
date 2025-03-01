namespace Sandbox.Images;

using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class EdgeDetection
{
    public void Detect(string imagePath)
    {
        var edgeDetection = Cv2.ImRead(imagePath);
        var imageDetect = new Mat();
        Cv2.Canny(edgeDetection, imageDetect, 50, 200); // 50, 200 is threshold value
        Window.ShowImages(imageDetect);
    }
}
