namespace Sandbox.Images;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;

internal class OpticalCharacterRecognition
{
    public void Recognize(string imagePath)
    {
        var ocr = new IronTesseract();
        ocr.Language = OcrLanguage.English;

        using (var input = new OcrInput())
        {
            input.LoadImage(imagePath);
            input.Deskew();
            var result = ocr.Read(input);
            Console.WriteLine(result);
        }
    }
}
