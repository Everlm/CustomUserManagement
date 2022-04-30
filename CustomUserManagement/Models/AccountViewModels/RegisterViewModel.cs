using CustomUserManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.Models.AccountViewModels
{
    public class RegisterViewModel
    {
      
        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="dd/MM/yyyy")]
        public DateTime Birthdate { get; set; } 

        [Required]
        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        //[ValidEmailDomain(allowedDomain: "pragimtech.com",
                            //ErrorMessage = "Email domain must be pragimtech.com")]
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
