using GestionProfesores.Shared.DTO;

namespace GestionProfesores.Client.Servicios
{
    public interface INotaServicio
    {
        Task<HttpRespuesta<List<NotaDTO>>> GetNotas();
        Task<HttpRespuesta<NotaDTO>> GetNota(int id);
        Task<HttpRespuesta<List<NotaDTO>>> GetNotasByAlumno(int alumnoId);
        Task<HttpRespuesta<List<NotaDTO>>> GetNotasByMateria(int materiaId);
        Task<HttpRespuesta<List<NotaDTO>>> GetNotasByAlumnoMateria(int alumnoId, int materiaId);
        Task<HttpRespuesta<decimal?>> GetPromedioAlumnoMateria(int alumnoId, int materiaId);
        Task<HttpRespuesta<int>> CrearNota(CrearNotaDTO nota);
        Task<HttpRespuesta<object>> ActualizarNota(NotaDTO nota);
        Task<HttpRespuesta<object>> EliminarNota(int id);
    }
}
