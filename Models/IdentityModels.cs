using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.Models
{
    public class IdentityModels
    {
        public class ApplicationUser : IdentityUser
        {
            public string UrlImagen { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public DateTime FechaAlta { get; set; }
            public bool Eliminado { get; set; }
         


        }
    }
}
