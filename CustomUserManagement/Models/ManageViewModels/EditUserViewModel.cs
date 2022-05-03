using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.Models.ManageViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "dd/MM/yyyy")]
        public DateTime Birthdate { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string City { get; set; }

        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}
