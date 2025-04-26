using API_REST_PRUEBA.DTOs;
using API_REST_PRUEBA.Repository;

namespace API_REST_PRUEBA.Services
{
    public class SalaCineService : ISalaCineService
    {
        private readonly ISalaCineRepository _salaCineRepository;
        private readonly IPeliculaSalaCineRepository _peliculaSalaCineRepository;

        public SalaCineService(ISalaCineRepository salaCineRepository, IPeliculaSalaCineRepository peliculaSalaCineRepository)
        {
            _salaCineRepository = salaCineRepository;
            _peliculaSalaCineRepository = peliculaSalaCineRepository;
        }

        public async Task<SalaStatusDto> GetSalaStatusByNameAsync(string nombreSala)
        {
            var sala = await _salaCineRepository.GetByNameAsync(nombreSala);
            if (sala == null || !sala.Activo)
            {
                return new SalaStatusDto { SalaNombre = nombreSala, Status = "Sala no encontrada o inactiva", PeliculasCount = 0 };
            }
            int count = await _peliculaSalaCineRepository.CountPeliculasActivasBySalaIdAsync(sala.IdSala, DateTime.Now);

            string statusMessage;
            if (count < 3)
            {
                statusMessage = "Sala disponible";
            }
            else if (count >= 3 && count <= 5)
            {
                statusMessage = $"Sala con {count} películas asignadas";
            }
            else
            {
                statusMessage = "Sala no disponible";
            }

            return new SalaStatusDto { SalaNombre = sala.Nombre, Status = statusMessage, PeliculasCount = count };
        }
    }
}
