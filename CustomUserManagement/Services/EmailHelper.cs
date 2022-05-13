using System;
using System.Net.Mail;

namespace CustomUserManagement.Services
{
    public class EmailHelper
    {
        //public bool SendEmail(string userEmail, string confirmationLink)
        //{
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = new MailAddress("you email");
        //    mailMessage.To.Add(new MailAddress(userEmail));

        //    mailMessage.Subject = "you email";
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.Body =confirmationLink;

        //    SmtpClient client = new SmtpClient();
        //    client.Credentials = new System.Net.NetworkCredential("you email ", "*******");
        //    client.EnableSsl = true;
        //    client.Host = "smtp.office365.com";
        //    client.Port = 587;

        //    try
        //    {
        //        client.Send(mailMessage);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
        //    return false;
        //}
    }
}
