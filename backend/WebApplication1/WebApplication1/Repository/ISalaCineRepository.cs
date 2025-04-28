using API_REST_PRUEBA.Models;

namespace API_REST_PRUEBA.Repository
{
    public interface ISalaCineRepository
    {
        //todas las salas
        Task<IEnumerable<SalaCine>> GetAllAsync();
        //por nombre
        Task<SalaCine?> GetByNameAsync(string name);
        //por id
        Task<SalaCine?> GetByIdAsync(int id);
        //nueva sala a cine
        Task AddAsync(SalaCine salaCine);
        //actualizar sala
        Task UpdateAsync(SalaCine salaCine);
        //borrado logico
        Task DeleteLogicalAsync(int id);
        //cine activa con el id que se especifique
        Task<bool> ExistsAsync(int id);
    }
}
