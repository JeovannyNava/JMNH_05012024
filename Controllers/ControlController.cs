using JMNH_05012024.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.Controllers
{
    public class ControlController : Controller
    {
        private readonly ApplicationDbContext db;
        private UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; set; }
        public ControlController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            db = context;
            _userManager = userManager;
            Configuration = configuration;
        }
        [Route("mis-materias")]
        [Authorize(Roles = "Alumno")]
        public async Task<IActionResult> MisMateriasAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var alumno = db.Alumnos.FirstOrDefault(x => x.UserName == userName);
            alumno.Materias = db.MateriasAlumnos.Where(x => x.IdAlumno == alumno.IdAlumno && !x.Materia.Eliminado).Select(x => x.Materia).ToList();

            return View(alumno);
        }
        public async Task<PartialViewResult> _AgregarMateriaAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var alumno = db.Alumnos.FirstOrDefault(x => x.UserName == userName);
            var materiasAlumno = db.MateriasAlumnos.Where(x => x.IdAlumno == alumno.IdAlumno).Select(x => x.Materia).ToList().Select(x => x.IdMateria);
            ViewBag.Materias = db.Materias.Where(x => !x.Eliminado && !materiasAlumno.Contains(x.IdMateria)).Select(x => new SelectListItem { Value = x.IdMateria.ToString(), Text = x.Nombre + " $" + x.Costo }).ToList(); ;
            return PartialView();
        }
        public async Task<JsonResult> InscribirMateria(int IdMateria)
        {
            using var dbContextTransaction = db.Database.BeginTransaction();
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userName = user.UserName;
                var alumno = db.Alumnos.FirstOrDefault(x => x.UserName == userName);
                var materia = db.Materias.Find(IdMateria);
                db.MateriasAlumnos.Add(new MateriasAlumno
                {
                    IdAlumno = alumno.IdAlumno,
                    IdMateria = materia.IdMateria
                });
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
        public async Task<JsonResult> BajaMateria(int IdMateria)
        {
            using var dbContextTransaction = db.Database.BeginTransaction();
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userName = user.UserName;
                var alumno = db.Alumnos.FirstOrDefault(x => x.UserName == userName);
                var materia = db.Materias.Find(IdMateria);
                var materiaAlumno = db.MateriasAlumnos.FirstOrDefault(x=> x.IdAlumno == alumno.IdAlumno && x.IdMateria == materia.IdMateria);
                db.MateriasAlumnos.RemoveRange(materiaAlumno);
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
        [Route("materias-alumno/{IdAlumno}")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult MateriasAlumno(int IdAlumno)
        {

            var alumno = db.Alumnos.FirstOrDefault(x => x.IdAlumno == IdAlumno);
            alumno.Materias = db.MateriasAlumnos.Where(x => x.IdAlumno == alumno.IdAlumno && !x.Materia.Eliminado).Select(x => x.Materia).ToList();
            return View(alumno);

     
        }
    }
}
