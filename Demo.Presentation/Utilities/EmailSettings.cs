using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Utilities
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587); // Enable SSL or TLs 
            Client.EnableSsl = true;
            // Sender /Reciever 
            Client.Credentials = new NetworkCredential("mohamedmahamoudali15@gmail.com", "qqyrcnjotqvfzqgc");
            Client.Send("mohamedmahamoudali15@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
