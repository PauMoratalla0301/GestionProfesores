using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProfesores.BD.Data.Entity
{
    public class Asistencia : EntityBase
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int MateriaId { get; set; }

        public DateOnly Fecha { get; set; }

        public bool Presente { get; set; }  // true = presente, false = ausente

        // Relaciones
        public Alumno Alumno { get; set; }
        public Materia Materia { get; set; }

        
    }
}
