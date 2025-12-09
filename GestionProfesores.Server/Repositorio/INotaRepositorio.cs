using GestionProfesores.BD.Data.Entity;
using GestionProfesores.Server.Repositorio;

public interface INotaRepositorio : IRepositorio<Nota>
{
    Task<List<Nota>> SelectByAlumnoId(int alumnoId);
    Task<List<Nota>> SelectByMateriaId(int materiaId);
    Task<List<Nota>> SelectByAlumnoMateria(int alumnoId, int materiaId);
    Task<decimal?> GetPromedioByAlumnoMateria(int alumnoId, int materiaId);
}