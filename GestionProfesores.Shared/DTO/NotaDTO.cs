using GestionProfesores.Shared.DTO;
using System.ComponentModel.DataAnnotations;


namespace GestionProfesores.Shared.DTO
{
    public class NotaDTO : CrearNotaDTO
    {
        public int Id { get; set; }
        public AlumnoDTO? Alumno { get; set; }
        public MateriaDTO? Materia { get; set; }
    }
}