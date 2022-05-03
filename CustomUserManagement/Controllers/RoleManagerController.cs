using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CustomUserManagement.Models.ManageViewModels;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CustomUserManagement.Models;
using CustomUserManagement.Utilities;
using System;
using NToastNotify;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace CustomUserManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _toastNotification;
        public RoleManagerController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, INotyfService toastNotification)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._toastNotification = toastNotification;
        }

        
        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            return View(await _roleManager.Roles.ToListAsync());
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
                IdentityRole identityRole = new()
                {
                    Name = Model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    _toastNotification.Success($"A success {identityRole.Name}");
                    return RedirectToAction("ListRoles");
                }
                else _toastNotification.Error($"An error occured");

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
            var role = await _roleManager.FindByIdAsync(id);

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
            var role = await _roleManager.FindByIdAsync(Model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Model.Id} cannot be found";
                return NotFound();
            }

            else
            {
                role.Name = Model.RoleName;

                // Update el rol usando UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

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
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NoFound");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);

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
            var role = await _roleManager.FindByNameAsync(name);

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