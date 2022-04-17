using Microsoft.AspNetCore.Identity;

namespace CustomUserManagement.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
     
    }
}
