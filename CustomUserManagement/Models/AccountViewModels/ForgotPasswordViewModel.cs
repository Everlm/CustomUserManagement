using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
