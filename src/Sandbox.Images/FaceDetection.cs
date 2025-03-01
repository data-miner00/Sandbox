namespace Sandbox.Images;

using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class FaceDetection
{
    /// <summary>
    /// Draws a rectangle when detected a face.
    /// </summary>
    /// <param name="imagePath">The image path.</param>
    /// <param name="protoTxt">The proto text file.</param>
    /// <param name="caffeModel">The caffe model.</param>
    public void Detect(string imagePath, string protoTxt, string caffeModel)
    {
        var faceDetection = CvDnn.ReadNetFromCaffe(protoTxt, caffeModel);
        var image = Cv2.ImRead(imagePath);
        OpenCvSharp.Size size = new(300, 300);
        image = image.Resize(size);

        Scalar scalar = new Scalar(104.0, 177.0, 123.0);
        var blob = CvDnn.BlobFromImage(image, 1, size, scalar, true);
        faceDetection.SetInput(blob);
        var detections = faceDetection.Forward();
        var detectionsMat = Mat.FromPixelData(detections.Size(2), detections.Size(3), MatType.CV_32F, detections.Ptr(0));

        for (int i = 0; i < detectionsMat.Rows; i++)
        {
            var confidence = detectionsMat.At<float>(i, 2);
            if (confidence > 0.7)
            {
                int x1 = (int)(detectionsMat.At<float>(i, 3) * size.Width);
                int y1 = (int)(detectionsMat.At<float>(i, 4) * size.Height);
                int x2 = (int)(detectionsMat.At<float>(i, 5) * size.Width);
                int y2 = (int)(detectionsMat.At<float>(i, 6) * size.Height);
                Cv2.Rectangle(image, new OpenCvSharp.Point(x1, y1), new OpenCvSharp.Point(x2, y2), Scalar.Black);
            }
        }
    }
}
