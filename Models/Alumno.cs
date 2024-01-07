using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.Models
{
    public class Alumno
    {
        [Key]
        public int IdAlumno { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }
        public string UserName { get; set; }
        public bool Eliminado{ get; set; }
        public List<Materia> Materias { get; set; }



    }
}
