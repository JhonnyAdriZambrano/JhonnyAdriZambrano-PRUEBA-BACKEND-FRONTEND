using API_REST_PRUEBA.DTOs;
using API_REST_PRUEBA.Models;

namespace API_REST_PRUEBA.Services
{
    public interface IAsignacionService
    {
        Task<PeliculaSalaCine> CrearAsignacionAsync(AsignacionCreateDto dto);
    }
}
