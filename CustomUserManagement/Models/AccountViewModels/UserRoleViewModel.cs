using System;
using System.Collections.Generic;

namespace CustomUserManagement.Models.AccountViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
