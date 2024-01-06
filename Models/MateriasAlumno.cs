using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.Models
{
    public class MateriasAlumno
    {
        [Key]
        public int IdMateriaAlumno { get; set; }
        public int? IdAlumno { get; set; }
        [ForeignKey("IdAlumno")]
        public virtual Alumno Alumno{ get; set; }
        public int? IdMateria { get; set; }
        [ForeignKey("IdMateria")]
        public virtual Materia Materia{ get; set; }
    }
}
