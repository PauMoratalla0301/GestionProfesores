using GestionProfesores.BD.Data;
using GestionProfesores.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestionProfesores.Server.Repositorio
{
    public class MateriaRepositorio : Repositorio<Materia>, IMateriaRepositorio
    {
        private readonly Context context;

        public MateriaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Materia?> SelectByNombre(string nombre)
        {
            return await context.Materias
                .FirstOrDefaultAsync(x => x.Nombre.ToLower() == nombre.ToLower());
        }

        public async Task<List<Materia>> SelectWithDetails()
        {
            return await context.Materias
                .Include(m => m.Notas)
                .Include(m => m.Asistencias)
                .ToListAsync();
        }
    }
}