using System.ComponentModel.DataAnnotations;

namespace CustomUserManagement.Models.ManageViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
