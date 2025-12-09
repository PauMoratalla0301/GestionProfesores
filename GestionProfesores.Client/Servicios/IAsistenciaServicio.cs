using GestionProfesores.Shared.DTO;

namespace GestionProfesores.Client.Servicios
{
    public interface IAsistenciaServicio
    {
        Task<HttpRespuesta<object>> ActualizarAsistencia(AsistenciaDTO asistencia);
        Task<HttpRespuesta<int>> CrearAsistencia(CrearAsistenciaDTO asistencia);
        Task<HttpRespuesta<object>> EliminarAsistencia(int id);
        Task<HttpRespuesta<AsistenciaDTO>> GetAsistencia(int id);
        Task<HttpRespuesta<List<AsistenciaDTO>>> GetAsistencias();
        Task<HttpRespuesta<List<AsistenciaDTO>>> GetAsistenciasByAlumno(int alumnoId);
        Task<HttpRespuesta<List<AsistenciaDTO>>> GetAsistenciasByFecha(DateOnly fecha);
        Task<HttpRespuesta<List<AsistenciaDTO>>> GetAsistenciasByMateria(int materiaId);
    }
}