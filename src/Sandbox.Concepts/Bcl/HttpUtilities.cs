namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Web;

    public static class HttpUtilities
    {
        public static void EncodeDecodeHtml()
        {
            var content = "hello <div>world</div>";

            // Encode
            var encodedContent = HttpUtility.HtmlEncode(content);
            var writer = new StringWriter();

            // Decode
            HttpUtility.HtmlDecode(encodedContent, writer);
            var decodedContent = writer.ToString();

            Console.WriteLine(decodedContent);
        }
    }
}
