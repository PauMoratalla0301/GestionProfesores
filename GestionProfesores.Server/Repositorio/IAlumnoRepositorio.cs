using GestionProfesores.BD.Data.Entity;

namespace GestionProfesores.Server.Repositorio
{
    public interface IAlumnoRepositorio : IRepositorio<Alumno>
    {
        Task<Alumno?> SelectByNombreCompleto(string nombre, string apellido);
        Task<List<Alumno>> SelectWithDetails();
    }
}