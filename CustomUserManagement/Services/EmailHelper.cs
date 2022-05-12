using System;
using System.Net.Mail;

namespace CustomUserManagement.Services
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("mooncodetest@outlook.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "mooncodetest@outlook.com";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body =confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("mooncodetest@outlook.com", "*******");
            client.EnableSsl = true;
            client.Host = "smtp.office365.com";
            client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
