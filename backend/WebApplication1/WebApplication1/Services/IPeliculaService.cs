using API_REST_PRUEBA.DTOs;

namespace API_REST_PRUEBA.Services
{
    public interface IPeliculaService
    {
        Task<IEnumerable<PeliculaDto>> GetAllPeliculasAsync();
        Task<PeliculaDto?> GetPeliculaByIdAsync(int id);
        Task<IEnumerable<PeliculaDto>> SearchPeliculasByNameAsync(string nombre);
        Task<IEnumerable<PeliculaDto>> GetPeliculasByFechaPublicacionAsync(DateTime fecha);
        Task<PeliculaDto> CreatePeliculaAsync(PeliculaCreateDto peliculaDto);
        Task<bool> UpdatePeliculaAsync(int id, PeliculaUpdateDto peliculaDto);
        Task<bool> DeletePeliculaAsync(int id);
    }
}
