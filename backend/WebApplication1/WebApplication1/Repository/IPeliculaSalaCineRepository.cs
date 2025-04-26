using API_REST_PRUEBA.Models;

namespace API_REST_PRUEBA.Repository
{
    public interface IPeliculaSalaCineRepository
    {
        Task<IEnumerable<Pelicula>> GetPeliculasByFechaPublicacionAsync(DateTime fecha);
        Task<int> CountPeliculasActivasBySalaIdAsync(int idSala,DateTime fechaReferencia);

    }
}
