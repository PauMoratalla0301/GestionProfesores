using GestionProfesores.BD.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionProfesores.Server.Repositorio
{
    public class Repositorio<E> : IRepositorio<E>
                 where E : class, IEntityBase
    {
        private readonly Context context;

        public Repositorio(Context context)
        {
            this.context = context;
        }

        public async Task<bool> Existe(int id)
        {
            var existe = await context.Set<E>()
                             .AnyAsync(x => x.Id == id);
            return existe;
        }

        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }

        public async Task<E> SelectById(int id)
        {
            E? entidad = await context.Set<E>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return entidad;
        }

        public async Task<int> Insert(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }

            var entidadExistente = await SelectById(id);

            if (entidadExistente == null)
            {
                return false;
            }

            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var entidad = await SelectById(id);

            if (entidad == null)
            {
                return false;
            }

            context.Set<E>().Remove(entidad);
            await context.SaveChangesAsync();
            return true;
        }
    }
}