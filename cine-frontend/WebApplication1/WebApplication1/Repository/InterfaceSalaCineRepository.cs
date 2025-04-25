using API_REST_PRUEBA.Models;

namespace API_REST_PRUEBA.Repository
{
    public interface InterfaceSalaCineRepository
    {
        Task<SalaCine?> GetByNameAsync(string name);
    }
}
