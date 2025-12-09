using GestionProfesores.Shared.DTO;
using System.ComponentModel.DataAnnotations;


public class AlumnoDTO : CrearAlumnoDTO
{
    public int Id { get; set; }
    public List<NotaDTO>? Notas { get; set; }
    public List<AsistenciaDTO>? Asistencias { get; set; }
}