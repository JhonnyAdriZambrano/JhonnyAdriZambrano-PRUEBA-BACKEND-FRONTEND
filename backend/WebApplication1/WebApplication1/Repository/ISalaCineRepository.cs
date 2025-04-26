using API_REST_PRUEBA.Models;

namespace API_REST_PRUEBA.Repository
{
    public interface ISalaCineRepository
    {
        Task<SalaCine?> GetByNameAsync(string name);
    }
}
