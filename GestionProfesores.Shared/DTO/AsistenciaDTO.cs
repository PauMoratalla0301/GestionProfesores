using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProfesores.Shared.DTO
{
    public class AsistenciaDTO : CrearAsistenciaDTO
    {
        public int Id { get; set; }
        public AlumnoDTO? Alumno { get; set; }
        public MateriaDTO? Materia { get; set; }
    }
}
