using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JMNH_05012024.Models.IdentityModels;

namespace JMNH_05012024.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Alumno> Alumnos{ get; set; }
        public DbSet<Materia> Materias { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }


    }
}
