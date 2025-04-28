using API_REST_PRUEBA.DTOs;

namespace API_REST_PRUEBA.Services
{
    public interface ISalaCineService
    {
        Task<IEnumerable<SalaCineDto>> GetAllSalasAsync();
        Task<SalaStatusDto> GetSalaStatusByNameAsync(string nombreSala);
        Task<SalaCineDto?> GetSalaByIdAsync(int id);
        Task<SalaCineDto> CreateSalaAsync(SalaCineCreateDto salaDto);
        Task<bool> UpdateSalaAsync(int id, SalaCineUpdateDto salaDto);
        Task<bool> DeleteSalaLogicalAsync(int id);
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}
