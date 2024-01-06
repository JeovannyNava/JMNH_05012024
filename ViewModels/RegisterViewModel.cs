using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.ViewModels
{
    public class RegisterViewModel
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Imagen { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int IdTipoUsuario { get; set; }
        public string IdUser { get; set; }
    }
}
