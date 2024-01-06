﻿using JMNH_05012024.Models;
using JMNH_05012024.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JMNH_05012024.Models.IdentityModels;

namespace JMNH_05012024.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext db;
        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
        }
        public IActionResult Login()
        {
            return View();
        }    
        public IActionResult LoginAdmin()
        {
            return View();
        }    
        public IActionResult LoginAlumno()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    var user2 = User.Identity.IsAuthenticated;
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Usuario o contraseña invalidos");

            }
            return View(user);
        }
        [HttpPost]
        
        public async Task<ActionResult> LoginAlumno(LoginViewModelAlumno model)
        {

           
            var user = _userManager.Users.Where(x => x.UserName == model.Nombre + model.Apellido).FirstOrDefault();

            if (user != null)
            {
                await _signInManager.SignInAsync(user, true);
                if (User.Identity.IsAuthenticated)
                {
               
                    return RedirectToAction("Index", "Home");
                }

            }

            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

       public ActionResult EliminarUsuario(string IdUser)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == IdUser);
            user.LockoutEnabled = true;
            db.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
