using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JMNH_05012024.ViewModels
{
    public class LoginViewModelAlumno
    {
        [Required(ErrorMessage = "Es necesario ingresar su nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Es necesario ingresar su apellido")]
        public string Apellido { get; set; }
    }
}
