using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CustomUserManagement.ViewModels;
using System.Threading.Tasks;

namespace CustomUserManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel Model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = Model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
          
            return View(Model);
        }


    }
}
