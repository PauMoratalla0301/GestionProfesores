using GestionProfesores.BD.Data.Entity;

namespace GestionProfesores.Server.Repositorio
{
    public interface IAsistenciaRepositorio
    {
        Task<List<Asistencia>> Select();
        Task<Asistencia?> SelectById(int id);

        Task<List<Asistencia>> SelectByAlumnoId(int alumnoId);
        Task<List<Asistencia>> SelectByMateriaId(int materiaId);
        Task<List<Asistencia>> SelectByFecha(DateOnly fecha);
        Task<Asistencia?> SelectByAlumnoMateriaFecha(int alumnoId, int materiaId, DateOnly fecha);

        Task<bool> Update(int id, Asistencia asistenciaExistente);
        Task<bool> Existe(int id);
        Task<bool> Delete(int id);
        Task<int> Insert(Asistencia asistencia);
    }
}