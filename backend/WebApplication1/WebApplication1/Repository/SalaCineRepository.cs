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

        public async Task<SalaCine?> GetByNameAsync(string nombre)
        {
            return await contexto.SalasCine
                                 .FirstOrDefaultAsync(sc => sc.Nombre.Equals(nombre) && sc.Activo);
        }

        public async Task DeleteLogicalAsync(int id)
        {
            var sala = await contexto.SalasCine.FindAsync(id);
            if (sala != null)
            {
                sala.Activo = false;
                contexto.Entry(sala).State = EntityState.Modified;
                await contexto.SaveChangesAsync();
            }
        }
    }
}
