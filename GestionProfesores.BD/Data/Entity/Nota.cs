using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProfesores.BD.Data.Entity
{
    public class Nota : EntityBase
    {
      
        public int AlumnoId { get; set; }
        public int MateriaId { get; set; }

        public decimal Valor { get; set; }
        public DateOnly Fecha { get; set; }

        // Relaciones
        public Alumno Alumno { get; set; }
        public Materia Materia { get; set; }
    }
}
