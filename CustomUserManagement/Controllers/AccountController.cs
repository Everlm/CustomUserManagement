﻿using CustomUserManagement.Models;
using CustomUserManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomUserManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copio la data de RegisterViewModel a IdentityUser o ApplicationUser(Extendido)
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    FullName = model.FullName,
                    Birthdate = model.Birthdate,
                    PhoneNumber = model.PhoneNumber,
                    City = model.City,
                    Email = model.Email
                                
                };

                // Almaceno los datos de usuario en la tabla IdentityUsers 
                var result = await userManager.CreateAsync(user, model.Password);

                // Si se crea correctamente inicia sesion con el usuario creado
                // Inicia sesion y  redirige al index con la accion de HomeController
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                // Si existe algun error, lo agrega al objeto ModelState 
                // se mostrara la validacion en el axistente de etiquetas
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            //No null
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
              
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    //Verificamos la Url si es local
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
