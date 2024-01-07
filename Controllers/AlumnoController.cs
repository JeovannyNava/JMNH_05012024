using JMNH_05012024.Models;
using JMNH_05012024.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JMNH_05012024.Models.IdentityModels;

namespace JMNH_05012024.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly ApplicationDbContext db;
        private UserManager<IdentityUser> _userManager;
        public IConfiguration Configuration { get; set; }
        public AlumnoController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            db = context;         
            _userManager = userManager;
            Configuration = configuration;
        }

        [Route("Alumno/{IdAlumno?}")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Alumno(int? IdAlumno)
        {
            var model = db.Alumnos.Find(IdAlumno) ?? new Alumno();


            return View(model);
        }
        [Route("Alumnos")]
        [Authorize(Roles= "SuperAdmin")]
        public IActionResult Alumnos()
        {

            return View();
        }
        [HttpPost]
        public JsonResult Alumnos(int draw, int start, int length, Dictionary<string, string>[] order, string buscador)
        {
            DataTableVM response = new DataTableVM()
            {

            };

            try
            {

                var PageNumber = (start / length);
                PageNumber += 1;
                var query = db.Alumnos.Where(x => !x.Eliminado &&
                (string.IsNullOrEmpty(buscador) || x.Nombre.ToLower().Contains(buscador.ToLower()) ||
                 string.IsNullOrEmpty(buscador) || x.ApellidoPaterno.ToLower().Contains(buscador.ToLower()) ||
                 string.IsNullOrEmpty(buscador) || x.ApellidoPaterno.ToLower().Contains(buscador.ToLower()))
                ).ToList();
                var alumnos = query.Skip(start).Take(length).ToList();
                var data = alumnos.Select(alumno => new Dictionary<string, string>()
                {
                    ["Nombre"] = alumno.Nombre,
                    ["ApellidoPaterno"] = alumno.ApellidoPaterno,
                    ["ApellidoMaterno"] = alumno.ApellidoMaterno,
                    ["IdAlumno"] = alumno.IdAlumno.ToString(),

                }).ToList();

                return Json(new
                {
                    data,
                    recordsTotal = query.Count(),
                    draw,
                    recordsFiltered = alumnos.Count()
                });
            }
            catch (Exception ex)
            {
                response.error = ex.ToString();
                return Json(response);
            }
        }
        [HttpPost]
        public async Task<JsonResult> SaveOrUpdateAlumnoAsync(Alumno alumno)
        {
            using var dbContextTransaction = db.Database.BeginTransaction();
            try
            {
                var userName = alumno.Nombre.Replace(" ", string.Empty) + alumno.ApellidoPaterno.Replace(" ", string.Empty);
                var existeAlumno = db.Users.FirstOrDefault(x => x.UserName == alumno.Nombre.Replace(" ", string.Empty) + alumno.ApellidoPaterno.Replace(" ", string.Empty))!=null;
                if (existeAlumno && alumno.IdAlumno==0)
                {
                    return Json(new { status = 201, message = "Ya existe un alumno con este nombre y apellido" });
                }
                alumno.UserName = userName;
                var aux = alumno.IdAlumno == 0 ? db.Alumnos.Add(alumno) : db.Update(alumno);

                var user = alumno.IdAlumno == 0 ? new ApplicationUser
                {
                    UserName = alumno.Nombre.Replace(" ", string.Empty) + alumno.ApellidoPaterno.Replace(" ", string.Empty),
                    Nombre = alumno.Nombre,
                    ApellidoPaterno = alumno.ApellidoPaterno,
                    ApellidoMaterno = alumno.ApellidoMaterno,
                    Email = "generico@gmail.com",
                    FechaAlta = DateTime.Now
                } : db.Users.FirstOrDefault(x => x.UserName == alumno.Nombre.Replace(" ", string.Empty) + alumno.ApellidoPaterno.Replace(" ", string.Empty));

                if (alumno.IdAlumno == 0)
                {
                    var result = await _userManager.CreateAsync(user, "Abcd-1234");
                     await _userManager.AddToRoleAsync(user, "Alumno");
                    if (!result.Succeeded)
                    {
                        dbContextTransaction.Rollback();
                        return Json(new { status = 500, message = "Error", error = "Error al crear el usuario" });
                    }
     
                }
                else
                {
                    
                    user.Nombre = alumno.Nombre;
                    user.ApellidoPaterno = alumno.ApellidoPaterno;
                    user.ApellidoMaterno = alumno.ApellidoMaterno;
                    user.UserName = alumno.Nombre.Replace(" ", string.Empty) + alumno.ApellidoPaterno.Replace(" ", string.Empty);
                    db.Update(user);
                   
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return Json(new { status = 500, message = "Error", error = ex.ToString() });
            }
            dbContextTransaction.Commit();
            return Json(new { status = 200, message = "Ok" });
        }
        public JsonResult EliminarAlumno(int IdAlumno)
        {
            using var dbContextTransaction = db.Database.BeginTransaction();
            try
            {
                var alumno = db.Alumnos.Find(IdAlumno);
                alumno.Eliminado = true;
                var user = (ApplicationUser)_userManager.Users.Where(x => x.UserName == alumno.Nombre.Replace(" ", string.Empty) + alumno.ApellidoPaterno.Replace(" ", string.Empty)).FirstOrDefault();
                user.Eliminado = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return Json(new { status = 500, message = "Error", error = ex.ToString() });
            }
            dbContextTransaction.Commit();
            return Json(new { status = 200, message = "Ok" });
        }
    }
}
