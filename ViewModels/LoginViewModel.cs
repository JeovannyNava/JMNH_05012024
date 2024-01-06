using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Es necesario ingresar su correo")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Es necesario ingresar su contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }


    }
}
