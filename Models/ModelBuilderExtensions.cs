using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JMNH_05012024.Models.IdentityModels;

namespace JMNH_05012024.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            //Se crea usuario maestro
            CreateUser();

            void CreateUser()
            {
                string ADMIN_ID = "72c7e21c-ac55-46b4-a2ee-df24af6b45e4";
                string ROLE_ID = "e52c169c-cd4a-473a-becc-0dba41c78423"; 
                string ROLE_IDAlumno = "e52c169c-cd4a-473a-becc-0dba41c78424";
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Id = ROLE_ID
                }); 
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {
                    Name = "Alumno",
                    NormalizedName = "ALUMNO",
                    Id = ROLE_IDAlumno
                });
                var appUser = new ApplicationUser
                {
                    Email = "jeovanny156@gmail.com",
                    EmailConfirmed = true,
                    Nombre = "Jeovanny",
                    ApellidoPaterno = "Nava",
                    ApellidoMaterno = "Hernandez",
                    UserName = "Master",
                    NormalizedUserName = "JEOVANNY156@GMAIL.COM",
                    Id = ADMIN_ID
                  
                };
                modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                {
                    RoleId = ROLE_ID,
                    UserId = ADMIN_ID
                });
                //set user password
                PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
                appUser.PasswordHash = ph.HashPassword(appUser, "Digipro-123");

                //seed user
                modelBuilder.Entity<ApplicationUser>().HasData(appUser);
            }



        }
    }
}
