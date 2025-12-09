using System.ComponentModel.DataAnnotations;

public class CrearMateriaDTO
{
    [Required] public string Nombre { get; set; } = string.Empty;
}