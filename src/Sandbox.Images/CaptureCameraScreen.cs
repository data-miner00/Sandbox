namespace Sandbox.Images;

using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class CaptureCameraScreen
{
    public void Handle()
    {
        using (var capture = new VideoCapture(0))
        using (var window = new Window())
        {
            while (Cv2.WaitKey(10) != 27)
            {
                window.Image = capture.RetrieveMat();
            }
        }
    }
}
