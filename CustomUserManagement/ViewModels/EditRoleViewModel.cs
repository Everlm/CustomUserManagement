using CustomUserManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.ViewModels
{
    public class EditRoleViewModel
    {
        
        public string Id  { get; set; }

        [Required]
        public string RoleName { get; set; }

        //public IEnumerable<ApplicationUser> Users  { get; set; }
    }
}
