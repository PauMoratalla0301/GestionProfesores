using GestionProfesores.Shared.DTO;

namespace GestionProfesores.Client.Servicios
{
    public interface IAlumnoServicio
    {
        Task<HttpRespuesta<List<AlumnoDTO>>> GetAlumnos();
        Task<HttpRespuesta<AlumnoDTO>> GetAlumno(int id);
        Task<HttpRespuesta<int>> CrearAlumno(CrearAlumnoDTO alumno);
        Task<HttpRespuesta<object>> ActualizarAlumno(AlumnoDTO alumno);
        Task<HttpRespuesta<object>> EliminarAlumno(int id);
        Task<HttpRespuesta<AlumnoDTO>> GetAlumnoByNombre(string nombre, string apellido);
    }
}