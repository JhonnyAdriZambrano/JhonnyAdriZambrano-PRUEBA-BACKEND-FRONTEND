using API_REST_PRUEBA.Models.DTOs;

namespace API_REST_PRUEBA.Services
{
    public interface ISalaCineService
    {
        Task<SalaStatusDto> GetSalaStatusByNameAsync(string nombreSala);
    }
}
