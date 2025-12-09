using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProfesores.Shared.DTO
{
    public class CrearAsistenciaDTO
    {
        [Required(ErrorMessage = "El ID del alumno es obligatorio")]
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "El ID de la materia es obligatorio")]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "El estado de asistencia es obligatorio")]
        public bool Presente { get; set; }
    }
}
