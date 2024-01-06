﻿using JMNH_05012024.Models;
using JMNH_05012024.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly ApplicationDbContext db;
        private UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; set; }
        public AlumnoController(ApplicationDbContext context, IWebHostEnvironment env, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            db = context;
            _env = env;
            _userManager = userManager;
            Configuration = configuration;
        }

        [Route("Alumno/{IdAlumno?}")]
        public IActionResult Alumno(int? IdAlumno)
        {
            var model = db.Alumnos.Find(IdAlumno) ?? new Alumno();


            return View(model);
        }
        [Route("Alumnos")]
        public IActionResult Alumnos()
        {

            return View();
        }
        [HttpPost]
        public JsonResult Alumnos(int draw, int start, int length, Dictionary<string, string>[] order, int IdCliente, string buscador)
        {
            DataTableVM response = new DataTableVM()
            {

            };

            try
            {

                var PageNumber = (start / length);
                PageNumber += 1;
                var query = db.Alumnos.Where(x =>
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
        public JsonResult SaveOrUpdateAlumno(Alumno alumno)
        {
            using var dbContextTransaction = db.Database.BeginTransaction();
            try
            {
                var aux = alumno.IdAlumno == 0 ? db.Alumnos.Add(alumno) : db.Update(alumno);
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