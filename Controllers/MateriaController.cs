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
        public IConfiguration _configuration { get; set; }
        public MateriaController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            db = context;
            _userManager = userManager;
            _configuration = configuration;
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
                var query = db.Materias.Where(x => !x.Eliminado &&
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
        [HttpPost]
        public JsonResult SaveOrUpdateMateria(Materia materia)
        {
            using var dbContextTransaction = db.Database.BeginTransaction();
            var listRemovidos = new List<string>();
            string nuevoNombre = materia.Nombre;
            try
            {
                var cadenaCaracteres = _configuration.GetValue <string>("CaracteresInvalidos:caracteres");
                
                foreach (char c in cadenaCaracteres)
                {
                    if (materia.Nombre.Contains(c))
                    {
                        nuevoNombre = nuevoNombre.Replace(c.ToString(), string.Empty);
                        listRemovidos.Add(c.ToString());
                    }
                    
                }
                materia.Nombre = nuevoNombre;
                var aux = materia.IdMateria == 0 ? db.Materias.Add(materia) : db.Update(materia);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return Json(new { status = 500, message = "Error", error = ex.ToString() });
            }
            dbContextTransaction.Commit();
            return Json(new { status = 200, message = "Ok", removidos = listRemovidos });
        } 
        public JsonResult EliminarMateria(int IdMateria)
        {
            using var dbContextTransaction = db.Database.BeginTransaction();
            try
            {
                var materia = db.Materias.Find(IdMateria);
                materia.Eliminado = true;
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
