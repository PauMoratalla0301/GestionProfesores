using GestionProfesores.BD.Data;
using GestionProfesores.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestionProfesores.Server.Repositorio
{
    public class NotaRepositorio : Repositorio<Nota>, INotaRepositorio
    {
        private readonly Context context;

        public NotaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Nota>> SelectByAlumnoId(int alumnoId)
        {
            return await context.Notas
                .Include(n => n.Materia)
                .Where(n => n.AlumnoId == alumnoId)
                .ToListAsync();
        }

        public async Task<List<Nota>> SelectByMateriaId(int materiaId)
        {
            return await context.Notas
                .Include(n => n.Alumno)
                .Where(n => n.MateriaId == materiaId)
                .ToListAsync();
        }

        public async Task<List<Nota>> SelectByAlumnoMateria(int alumnoId, int materiaId)
        {
            return await context.Notas
                .Where(n => n.AlumnoId == alumnoId && n.MateriaId == materiaId)
                .OrderBy(n => n.Fecha)
                .ToListAsync();
        }

        public async Task<decimal?> GetPromedioByAlumnoMateria(int alumnoId, int materiaId)
        {
            var promedio = await context.Notas
                .Where(n => n.AlumnoId == alumnoId && n.MateriaId == materiaId)
                .AverageAsync(n => (double?)n.Valor);

            return (decimal?)promedio;
        }
    }
}