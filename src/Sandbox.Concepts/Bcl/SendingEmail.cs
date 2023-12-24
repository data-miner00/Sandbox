namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Net.Mail;
    using System.Net.Mime;

    public static class SendingEmail
    {
        public static void SendEmail(string to, string from)
        {
            var message = new MailMessage(from, to);
            message.Subject = "Testing the SmtpClient class";
            message.Body = "Lorem ipsum dolor sit amet";

            // Add a carbon copy recipient
            var copy = new MailAddress("mumkhong@contoso.com");
            message.CC.Add(copy);

            var client = new SmtpClient("smtp.freesmtpservers.com" /*"smtp.gmail.com"*/)
            {
                Port = 25,
                // Credentials = new NetworkCredential("email", "pass"),
                // EnableSsl = true,
            };

            Console.WriteLine("Send an email message to {0} using the SMTP host {1}", to, client.Host);

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SendEmailWithAttachment(string to, string from, string filename)
        {
            var message = new MailMessage(from, to)
            {
                Subject = "Testing the SmtpClient class",
                Body = "Lorem ipsum dolor sit amet",
            };

            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var contentType = new ContentType(MediaTypeNames.Text.Plain);

            // Attach the file stream to email
            var data = new Attachment(fileStream, contentType);

            message.Attachments.Add(data);

            var client = new SmtpClient("smtp.freesmtpservers.com" /*"smtp.gmail.com"*/)
            {
                Port = 25,
                // Credentials = new NetworkCredential("email", "pass"),
                // EnableSsl = true,
            };

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                data.Dispose();
            }
        }
    }
}
