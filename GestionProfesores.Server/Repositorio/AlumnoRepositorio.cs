using GestionProfesores.BD.Data;
using GestionProfesores.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestionProfesores.Server.Repositorio
{
    public class AlumnoRepositorio : Repositorio<Alumno>, IAlumnoRepositorio
    {
        private readonly Context context;

        public AlumnoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Alumno?> SelectByNombreCompleto(string nombre, string apellido)
        {
            return await context.Alumnos
                .FirstOrDefaultAsync(x =>
                    x.Nombre.ToLower() == nombre.ToLower() &&
                    x.Apellido.ToLower() == apellido.ToLower());
        }

        public async Task<List<Alumno>> SelectWithDetails()
        {
            return await context.Alumnos
                .Include(a => a.Notas)
                .Include(a => a.Asistencias)
                .ToListAsync();
        }
    }
}