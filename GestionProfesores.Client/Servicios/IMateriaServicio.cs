using GestionProfesores.Shared.DTO;

namespace GestionProfesores.Client.Servicios
{
    public interface IMateriaServicio
    {
        Task<HttpRespuesta<List<MateriaDTO>>> GetMaterias();
        Task<HttpRespuesta<MateriaDTO>> GetMateria(int id);
        Task<HttpRespuesta<MateriaDTO>> GetMateriaByNombre(string nombre);
        Task<HttpRespuesta<int>> CrearMateria(CrearMateriaDTO materia);
        Task<HttpRespuesta<object>> ActualizarMateria(MateriaDTO materia);
        Task<HttpRespuesta<object>> EliminarMateria(int id);
    }
}
