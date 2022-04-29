using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
