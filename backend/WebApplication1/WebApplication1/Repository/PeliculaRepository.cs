using API_REST_PRUEBA.Data;
using API_REST_PRUEBA.Models;
using Microsoft.EntityFrameworkCore;

namespace API_REST_PRUEBA.Repository
{
    public class PeliculaRepository: InterfacePeliculaRepository
    {
        private readonly CineDBContext contexto;

        public PeliculaRepository(CineDBContext context)
        {
            contexto = context;
        }
        // Devolver películas activas
        public async Task<IEnumerable<Pelicula>> GetAllAsync()
        {
            return await contexto.Peliculas.Where(p => p.Activo).ToListAsync();
        }
        // Buscar por ID y este activa
        public async Task<Pelicula?> GetByIdAsync(int id)
        {
            return await contexto.Peliculas.FirstOrDefaultAsync(p => p.IdPelicula == id && p.Activo);
        }
        public async Task<IEnumerable<Pelicula>> GetByNameAsync(string nombre)
        {
            // Buscar por nombre mayúsculas/minúsculas estando activa
            return await contexto.Peliculas
                                 .Where(p => p.Nombre.Contains(nombre) && p.Activo)
                                 .ToListAsync();
        }

        public async Task AddAsync(Pelicula pelicula)
        {
            pelicula.Activo = true; 
            contexto.Peliculas.Add(pelicula);
            await contexto.SaveChangesAsync(); // Guardado en db
        }

        public async Task UpdateAsync(Pelicula pelicula)
        {
            contexto.Entry(pelicula).State = EntityState.Modified;

            contexto.Entry(pelicula).Property(p => p.Activo).IsModified = false;
            await contexto.SaveChangesAsync();
        }

        public async Task DeleteLogicalAsync(int id)
        {
            var pelicula = await contexto.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                pelicula.Activo = false; // Cambiar el estado a inactivo
                contexto.Entry(pelicula).State = EntityState.Modified;
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await contexto.Peliculas.AnyAsync(p => p.IdPelicula == id && p.Activo);
        }
    }
}
