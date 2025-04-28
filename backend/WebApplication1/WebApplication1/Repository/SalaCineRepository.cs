using API_REST_PRUEBA.Data;
using API_REST_PRUEBA.Models;
using Microsoft.EntityFrameworkCore;

namespace API_REST_PRUEBA.Repository
{
    public class SalaCineRepository : ISalaCineRepository
    {
        private readonly CineDBContext contexto;
        public SalaCineRepository(CineDBContext context)
        {
            contexto = context;
        }
        public async Task<IEnumerable<SalaCine>> GetAllAsync()
        {
            return await contexto.SalasCine.Where(sc => sc.Activo).ToListAsync();
        }
        public async Task<SalaCine?> GetByIdAsync(int id)
        {
            return await contexto.SalasCine.FirstOrDefaultAsync(sc => sc.IdSala == id && sc.Activo);
        }
        public async Task AddAsync(SalaCine salaCine)
        {
            salaCine.Activo = true;
            await contexto.SalasCine.AddAsync(salaCine);
        }
        public async Task UpdateAsync(SalaCine salaCine)
        {
            contexto.Entry(salaCine).State = EntityState.Modified;
            contexto.Entry(salaCine).Property(p => p.Activo).IsModified = false;
        }
        public async Task DeleteLogicalAsync(int id)
        {
            var sala = await contexto.SalasCine.FindAsync(id);
            if (sala != null)
            {
                sala.Activo = false;
                contexto.Entry(sala).State = EntityState.Modified;
            }
        }
        public async Task<SalaCine?> GetByNameAsync(string nombre)
        {
            // Busca por nombre
            return await contexto.SalasCine
                                 .FirstOrDefaultAsync(sc => sc.Nombre == nombre && sc.Activo);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await contexto.SalasCine.AnyAsync(sc => sc.IdSala == id && sc.Activo);
        }
    }
}
