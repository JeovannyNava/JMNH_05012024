using JMNH_05012024.Models;
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
        public IActionResult Usuarios()
        {
            var users = db.Users.Where(x => !x.LockoutEnabled).ToList().Select(x => new Usuario()
            {
                Nombre = x.Nombre,
                ApellidoPaterno = x.ApellidoPaterno,
                ApellidoMaterno = x.ApellidoMaterno,
                IdUser = x.Id
            }).ToList();

            return View(users);
        }

        public async Task<JsonResult> RegistrarUsuario(RegisterViewModel model)
        {

           
            var user = string.IsNullOrEmpty(model.IdUser) ? new ApplicationUser
            {
                UserName = model.Email,
                Nombre = model.Nombre,
                ApellidoPaterno = model.ApellidoPaterno,
                ApellidoMaterno = model.ApellidoMaterno,
                Email = model.Email,
                FechaAlta = DateTime.Now
            } : db.Users.FirstOrDefault(x => x.Id == model.IdUser);

            if (string.IsNullOrEmpty(model.IdUser))
            {
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Json(new { Status = 200, Message = "Ok" });
                }
            }
            else
            {
                user.Nombre = model.Nombre;
                user.ApellidoPaterno = model.ApellidoPaterno;
                user.ApellidoMaterno = model.ApellidoMaterno;
                db.SaveChanges();
                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    if (model.Password == model.ConfirmPassword)
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }
                }
                return Json(new { Status = 200, Message = "Ok" });
            }
            return Json(new { Status = 500, Message = "Failed" });
        }

        [Route("/Usuario/{IdUsuario?}")]
        public IActionResult Usuario(string IdUsuario)
        {

            var user = new ApplicationUser();
            user = (ApplicationUser)db.Users.Find(IdUsuario);
            var model = new RegisterViewModel();
            if (user != null)
            {
                model.Email = user.Email;
                model.Nombre = user.Nombre;
                model.Imagen = user.UrlImagen;
                model.ApellidoPaterno = user.ApellidoPaterno;
                model.ApellidoMaterno = user.ApellidoMaterno;
                model.IdUser = user.Id;
            }
            ViewBag.IdUser = _userManager.GetUserId(User);
            return View(model);
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
