using Microsoft.AspNetCore.Identity;
using System;

namespace CustomUserManagement.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string City { get; set; }

    }
}
