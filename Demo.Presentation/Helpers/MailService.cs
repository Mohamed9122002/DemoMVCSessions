using Demo.Presentation.Settings;
using Demo.Presentation.Utilities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Demo.Presentation.Helpers
{
    public class MailService(IOptions<MailSetting> options) : IMailService
    {
        private readonly MailSetting _options = options.Value;

        public void Send(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject
            };
            // To
            mail.To.Add(MailboxAddress.Parse(email.To));
            //  From 
            mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));
            // Create Body 
            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            mail.Body = builder.ToMessageBody();
            // Connect Mail Server 
            using var smtp = new SmtpClient();
            smtp.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Email, _options.Password);

            smtp.Send(mail);

            smtp.Disconnect(true);

        }
    }
}
