using System;
using System.Collections.Generic;

namespace CustomUserManagement.Models.ManageViewModels
{
    public class UserListViewModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
