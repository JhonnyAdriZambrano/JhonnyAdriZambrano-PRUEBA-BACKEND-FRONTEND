using System.Data;
using API_REST_PRUEBA.Data;
using API_REST_PRUEBA.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API_REST_PRUEBA.Repository
{
    public class PeliculaSalaCineRepository : IPeliculaSalaCineRepository
    {
        private readonly CineDBContext contexto;

        public PeliculaSalaCineRepository(CineDBContext context)
        {
            contexto = context;
        }
        public async Task<int> CountPeliculasActivasBySalaIdAsync(int idSala, DateTime fechaReferencia)
        {
            return await contexto.PeliculasSalasCine
                                 .Where(psc =>
                                     psc.IdSalaCine == idSala &&
                                     psc.FechaPublicacion.Date <= fechaReferencia.Date &&
                                     psc.FechaFin.Date >= fechaReferencia.Date &&
                                     psc.Pelicula.Activo &&
                                     psc.SalaCine.Activo)
                                 .Select(psc => psc.IdPelicula)
                                 .Distinct()
                                 .CountAsync();

        }

        public async Task<IEnumerable<Pelicula>> GetPeliculasByFechaPublicacionConSPAsync(DateTime fecha)
        {
            var fechaParam = new SqlParameter("@FechaBusqueda", SqlDbType.Date) { Value = fecha.Date };
            
            return await contexto.Peliculas
                                   .FromSqlRaw("EXEC sp_GetPeliculasPorFechaPublicacion @FechaBusqueda", fechaParam)
                                   .ToListAsync();
        }
        public async Task<IEnumerable<Pelicula>> GetPeliculasByFechaPublicacionAsync(DateTime fecha)
        {
            return await contexto.PeliculasSalasCine

                                 .Where(psc => psc.FechaPublicacion.Date == fecha.Date &&
                                               psc.Pelicula.Activo &&
                                               psc.SalaCine.Activo)
                                 .Select(psc => psc.Pelicula)
                                 .Distinct()
                                 .ToListAsync();
        }

    }
}