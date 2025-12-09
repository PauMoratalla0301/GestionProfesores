using GestionProfesores.BD.Data.Entity;
using GestionProfesores.Server.Repositorio;

public interface IMateriaRepositorio : IRepositorio<Materia>
{
    Task<Materia?> SelectByNombre(string nombre);
    Task<List<Materia>> SelectWithDetails();
}