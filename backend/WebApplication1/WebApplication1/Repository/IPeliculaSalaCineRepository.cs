using API_REST_PRUEBA.Models;

namespace API_REST_PRUEBA.Repository
{
    public interface IPeliculaSalaCineRepository
    {
        Task<int> CountPeliculasActivasBySalaIdAsync(int idSala, DateTime fechaReferencia);
        Task<IEnumerable<Pelicula>> GetPeliculasByFechaPublicacionAsync(DateTime fecha);
        Task<IEnumerable<Pelicula>> GetPeliculasByFechaPublicacionConSPAsync(DateTime fecha);
        Task AddAsignacionAsync(PeliculaSalaCine asignacion);
        Task<bool> CheckOverlapAsync(int idPelicula, int idSalaCine, DateTime fechaInicio, DateTime fechaFin);
    }
}