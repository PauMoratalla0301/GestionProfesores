using GestionProfesores.BD.Data;
using GestionProfesores.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestionProfesores.Server.Repositorio
{
    public class AsistenciaRepositorio : Repositorio<Asistencia>, IAsistenciaRepositorio
    {
        private readonly Context context;

        public AsistenciaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        // :small_blue_diamond: Obtener todas las asistencias
        public async Task<List<Asistencia>> Select()
        {
            return await context.Asistencias
                .Include(a => a.Alumno)
                .Include(a => a.Materia)
                .ToListAsync();
        }

        // :small_blue_diamond: Obtener una asistencia por ID
        public async Task<Asistencia?> SelectById(int id)
        {
            return await context.Asistencias
                .Include(a => a.Alumno)
                .Include(a => a.Materia)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // :small_blue_diamond: Asistencias por alumno
        public async Task<List<Asistencia>> SelectByAlumnoId(int alumnoId)
        {
            return await context.Asistencias
                .Include(a => a.Materia)
                .Where(a => a.AlumnoId == alumnoId)
                .ToListAsync();
        }

        // :small_blue_diamond: Asistencias por materia
        public async Task<List<Asistencia>> SelectByMateriaId(int materiaId)
        {
            return await context.Asistencias
                .Include(a => a.Alumno)
                .Where(a => a.MateriaId == materiaId)
                .ToListAsync();
        }

        // :small_blue_diamond: Asistencias por fecha
        public async Task<List<Asistencia>> SelectByFecha(DateOnly fecha)
        {
            return await context.Asistencias
                .Include(a => a.Alumno)
                .Include(a => a.Materia)
                .Where(a => a.Fecha == fecha)
                .ToListAsync();
        }

        // :small_blue_diamond: Una asistencia puntual (alumno + materia + fecha)
        public async Task<Asistencia?> SelectByAlumnoMateriaFecha(int alumnoId, int materiaId, DateOnly fecha)
        {
            return await context.Asistencias
                .FirstOrDefaultAsync(a =>
                    a.AlumnoId == alumnoId &&
                    a.MateriaId == materiaId &&
                    a.Fecha == fecha);
        }

        // :small_blue_diamond: Actualizar asistencia
        public async Task<bool> Update(int id, Asistencia asistenciaExistente)
        {
            if (id != asistenciaExistente.Id)
                return false;

            context.Asistencias.Update(asistenciaExistente);
            var filas = await context.SaveChangesAsync();
            return filas > 0;   // true si se actualizó algo
        }

    }
}