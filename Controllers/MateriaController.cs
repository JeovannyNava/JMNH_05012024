using JMNH_05012024.Models;
using JMNH_05012024.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.Controllers
{
    public class MateriaController : Controller
    {
        private readonly ApplicationDbContext db;
        private UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; set; }
        public MateriaController(ApplicationDbContext context, IWebHostEnvironment env, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            db = context;
            _env = env;
            _userManager = userManager;
            Configuration = configuration;
        }

        [Route("Materia/{IdMateria?}")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Materia(int? IdMateria)
        {
            var model = db.Materias.Find(IdMateria) ?? new Materia();


            return View(model);
        }
        [Route("Materias")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Materias()
        {

            return View();
        }
        [HttpPost]
        public JsonResult Materias(int draw, int start, int length, Dictionary<string, string>[] order, string buscador)
        {
            DataTableVM response = new DataTableVM()
            {

            };

            try
            {

                var PageNumber = (start / length);
                PageNumber += 1;
                var query = db.Materias.Where(x =>
                (string.IsNullOrEmpty(buscador) || x.Nombre.ToLower().Contains(buscador.ToLower()))).ToList();
                var materias = query.Skip(start).Take(length).ToList();
                var data = materias.Select(materia => new Dictionary<string, string>()
                {
                    ["Nombre"] = materia.Nombre,
                    ["Costo"] = materia.Costo.ToString(),
                    ["IdMateria"] = materia.IdMateria.ToString()

                }).ToList();

                return Json(new
                {
                    data,
                    recordsTotal = query.Count(),
                    draw,
                    recordsFiltered = materias.Count()
                });
            }
            catch (Exception ex)
            {
                response.error = ex.ToString();
                return Json(response);
            }
        }
        [Route("mis-materias")]
        [Authorize(Roles="Alumno")]
        public async Task<IActionResult> MisMateriasAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var alumno = db.Alumnos.FirstOrDefault(x=> x.UserName == userName);
            alumno.Materias = db.MateriasAlumnos.Where(x=> x.IdAlumno == alumno.IdAlumno).Select(x=> x.Materia).ToList();
            
            return View(alumno);
        }
        [HttpPost]
        public JsonResult SaveOrUpdateMateria(Materia materia)
        {
            using var dbContextTransaction = db.Database.BeginTransaction();
            try
            {
                var aux = materia.IdMateria == 0 ? db.Materias.Add(materia) : db.Update(materia);
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
        public async Task<PartialViewResult> _AgregarMateriaAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var alumno = db.Alumnos.FirstOrDefault(x => x.UserName == userName);
            var materiasAlumno =  db.MateriasAlumnos.Where(x => x.IdAlumno == alumno.IdAlumno).Select(x => x.Materia).ToList().Select(x=> x.IdMateria);
            ViewBag.Materias = db.Materias.Where(x => !materiasAlumno.Contains(x.IdMateria)).Select(x => new SelectListItem { Value = x.IdMateria.ToString(), Text = x.Nombre + " $" + x.Costo }).ToList(); ;
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
                db.MateriasAlumnos.Add(new MateriasAlumno { 
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
    }
}
