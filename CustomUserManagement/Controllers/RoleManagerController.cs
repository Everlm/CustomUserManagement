using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CustomUserManagement.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CustomUserManagement.Models;
using CustomUserManagement.Utilities;
using System;
using CustomUserManagement.Enums;
using CustomUserManagement.Extensions;

namespace CustomUserManagement.Controllers
{
    public class RoleManagerController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            return View(await roleManager.Roles.ToListAsync());
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

                    Notify("Data saved successfully");
                    return RedirectToAction("ListRoles");


                }
                else
                {
                    Notify("Could not delete data!", notificationType: NotificationType.error);
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(Model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NoFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel Model)
        {
            var role = await roleManager.FindByIdAsync(Model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Model.Id} cannot be found";
                return NotFound();
            }

            else
            {
                role.Name = Model.RoleName;

                // Update el rol usando UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(Model);

            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NoFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("ListRoles");

        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsRoleUse(string name)
        {
            var role = await roleManager.FindByNameAsync(name);

            if (role == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {name} is already exist");
            }
        }

    }
}