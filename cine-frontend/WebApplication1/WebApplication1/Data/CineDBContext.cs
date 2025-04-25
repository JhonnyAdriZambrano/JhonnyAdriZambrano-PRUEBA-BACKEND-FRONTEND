using API_REST_PRUEBA.Models;
using Microsoft.EntityFrameworkCore;

namespace API_REST_PRUEBA.Data
{
    public class CineDBContext : DbContext
    {
        public CineDBContext(DbContextOptions<CineDBContext> options) : base(options)
        {
        }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<SalaCine> SalasCine { get; set; }
        public DbSet<PeliculaSalaCine> PeliculasSalasCine { get; set; }

    }
}
