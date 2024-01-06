using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.Models
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        public double Costo { get; set; }
    }
}
