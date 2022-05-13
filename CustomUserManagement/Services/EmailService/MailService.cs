using CustomUserManagement.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace CustomUserManagement.Services.EmailService
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string userEmail, string confirmationLink)
        {
            
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = _mailSettings.Mail;
           
            var builder = new BodyBuilder();

            //builder.TextBody = string.Format(@"Hello{0}.",userEmail);
            builder.HtmlBody = string.Format(@"<p>Hello {0},<br> To activate your account, please click <a href={1}> here</a> to verify your email address.", userEmail,confirmationLink);      
            
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
