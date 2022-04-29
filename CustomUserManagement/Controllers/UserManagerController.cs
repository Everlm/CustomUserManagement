using CustomUserManagement.Models;
using CustomUserManagement.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CustomUserManagement.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManagerController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager; 
        }
        public async Task <IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            var userRoleViewModel = new List<UserRoleViewModel>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserRoleViewModel();

                thisViewModel.UserId = user.Id;
                thisViewModel.FullName = user.FullName;
                thisViewModel.Birthdate = user.Birthdate;
                thisViewModel.City = user.City;
                thisViewModel.Email = user.Email;
               
                thisViewModel.Roles = await GetUserRoles(user);
                userRoleViewModel.Add(thisViewModel);

            }
            return View(userRoleViewModel);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }
    }
}
