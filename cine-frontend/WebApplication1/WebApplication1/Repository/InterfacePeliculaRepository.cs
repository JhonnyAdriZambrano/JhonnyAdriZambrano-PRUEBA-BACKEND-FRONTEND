using API_REST_PRUEBA.Models;

namespace API_REST_PRUEBA.Repository
{
    public interface InterfacePeliculaRepository
    {
        Task<IEnumerable<Pelicula>> GetAllAsync();
        Task<Pelicula?> GetByIdAsync(int id);
        Task<IEnumerable<Pelicula>> GetByNameAsync(string nombre);
        Task AddAsync(Pelicula pelicula);
        Task UpdateAsync(Pelicula pelicula);
        Task DeleteLogicalAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
