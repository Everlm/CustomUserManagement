using CustomUserManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.Models.ManageViewModels
{
    public class EditRoleViewModel
    {
       
        public string Id { get; set; }

        [Required(ErrorMessage ="RoleName is required")]
        public string RoleName { get; set; }

    }
}
