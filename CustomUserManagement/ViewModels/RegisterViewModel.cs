using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.ViewModels
{
    public class RegisterViewModel
    {


        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

   
        [DataType(DataType.Password)]
        [Display(Name = " Confirm Password")]
        [Compare("Password", ErrorMessage ="Password no match")]
        public string ConfirmPassword { get; set; }

    }
}
