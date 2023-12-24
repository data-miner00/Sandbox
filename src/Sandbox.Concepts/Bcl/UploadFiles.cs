namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public static class UploadFiles
    {
        public static void Example()
        {
            var uri = "ftp://ftp2.somesite.com";
            var credentials = new NetworkCredential("username", "password");

            var ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
            ftpRequest.Credentials = credentials;
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;

            using var response = (FtpWebResponse)ftpRequest.GetResponse();
            using var streamReader = new StreamReader(response.GetResponseStream());

            List<string> content = [];
            var line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                content.Add(line);
                Console.WriteLine(line);
                line = streamReader.ReadLine();
            }
        }

        public static void UploadFile(string filename)
        {
            var uri = "ftp://ftp2.somesite.com";
            var credentials = new NetworkCredential("username", "password");

            var ftpRequest = (FtpWebRequest)WebRequest.Create($"{uri}/{filename}");
            ftpRequest.Credentials = credentials;
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            var sourceStream = new StreamReader(filename);

            var fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();

            ftpRequest.ContentLength = fileContents.Length;

            var requestStream = ftpRequest.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            var response = (FtpWebResponse)ftpRequest.GetResponse();
            Console.WriteLine("Upload completed " + response.StatusDescription);
        }

        public static void DownloadFile(string filename)
        {
            var bytesRead = 0;
            var buffer = new byte[2048];

            var uri = "ftp://ftp2.somesite.com";
            var credentials = new NetworkCredential("username", "password");

            var ftpRequest = (FtpWebRequest)WebRequest.Create($"{uri}/{filename}");
            ftpRequest.Credentials = credentials;
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            var reader = ftpRequest.GetResponse().GetResponseStream();
            var fileStream = new FileStream(filename, FileMode.Create);

            while (true)
            {
                bytesRead = reader.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }

                fileStream.Write(buffer, 0, bytesRead);
            }

            fileStream.Close();
            Console.WriteLine();
        }
    }
}
