using System.ComponentModel.DataAnnotations;

namespace GestionProfesores.Shared.DTO
{
    public class CrearNotaDTO
    {
        [Required(ErrorMessage = "El ID del alumno es obligatorio")]
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "El ID de la materia es obligatorio")]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "El valor de la nota es obligatorio")]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateOnly Fecha { get; set; }
    }
}