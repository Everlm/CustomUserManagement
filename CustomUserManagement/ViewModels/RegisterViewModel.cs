using System;
using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.ViewModels
{
    public class RegisterViewModel
    {
      
        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; } = DateTime.Now.Date;

        [Required]
        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

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
