using API_REST_PRUEBA.DTOs;

namespace API_REST_PRUEBA.Services
{
    public interface ISalaCineService
    {
        Task<SalaStatusDto> GetSalaStatusByNameAsync(string nombreSala);
    }
}
