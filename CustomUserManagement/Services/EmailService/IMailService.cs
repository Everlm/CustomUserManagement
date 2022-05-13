using System.Threading.Tasks;

namespace CustomUserManagement.Services.EmailService
{
    public interface IMailService
    {
        Task SendEmailAsync(string userEmail, string confirmationLink);
    }
}
