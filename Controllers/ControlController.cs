using JMNH_05012024.Models;
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
    public class ControlController : Controller
    {
        private readonly ApplicationDbContext db;
        private UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; set; }
        public ControlController(ApplicationDbContext context, IWebHostEnvironment env, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            db = context;
            _env = env;
            _userManager = userManager;
            Configuration = configuration;
        }
        public IActionResult Alta()
        {
            return View();
        }
        [Route("materias-alumno/{IdAlumno?}")]
        public IActionResult MateriasAlumno(int? IdAlumno)
        {
            var materiasAlumno = db.MateriasAlumnos.Where(x => x.IdAlumno == IdAlumno).ToList();
            return View(materiasAlumno);
        }   
        [Route("alta-materia/{IdAlumno?}")]
        public IActionResult AltaMateria()
        {
            var materias = db.Materias.ToList();
            return View(materias);
        }
    }
}
