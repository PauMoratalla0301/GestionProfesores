using System.ComponentModel.DataAnnotations;

public class CrearAlumnoDTO
{
    [Required] public string Nombre { get; set; } = string.Empty;
    [Required] public string Apellido { get; set; } = string.Empty;
}