using API_REST_PRUEBA.Data;
using API_REST_PRUEBA.DTOs;
using API_REST_PRUEBA.Models;
using API_REST_PRUEBA.Repository;


namespace API_REST_PRUEBA.Services
{
    public class SalaCineService : ISalaCineService
    {
        private readonly ISalaCineRepository _salaCineRepository;
        private readonly IPeliculaSalaCineRepository _peliculaSalaCineRepository;
        private readonly IPeliculaRepository _peliculaRepository;
        private readonly CineDBContext _context;

        public SalaCineService(
            ISalaCineRepository salaCineRepository,
            IPeliculaSalaCineRepository peliculaSalaCineRepository,
            IPeliculaRepository peliculaRepository,
            CineDBContext context)
        {
            _salaCineRepository = salaCineRepository;
            _peliculaSalaCineRepository = peliculaSalaCineRepository;
            _peliculaRepository = peliculaRepository;
            _context = context;
        }

        public async Task<IEnumerable<SalaCineDto>> GetAllSalasAsync()
        {
            var salasEntities = await _salaCineRepository.GetAllAsync();
            var salasDtos = salasEntities.Select(sala => new SalaCineDto
            {
                IdSala = sala.IdSala,
                Nombre = sala.Nombre,
                Estado = sala.Estado
            }).ToList();

            return salasDtos;
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

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var peliculasActivas = await _peliculaRepository.GetAllAsync();
            int totalPeliculas = peliculasActivas.Count();

            //Obtener todas las salas activas
            var salasActivas = await _salaCineRepository.GetAllAsync();
            int totalSalas = salasActivas.Count();

            int salasDisponibles = 0;
            DateTime ahora = DateTime.Now;
            foreach (var sala in salasActivas)
            {
                int countAsignaciones = await _peliculaSalaCineRepository.CountPeliculasActivasBySalaIdAsync(sala.IdSala, ahora);
                if (countAsignaciones < 3)
                {
                    salasDisponibles++;
                }
            }
            return new DashboardStatsDto
            {
                TotalPeliculasActivas = totalPeliculas,
                TotalSalasActivas = totalSalas,
                TotalSalasDisponibles = salasDisponibles
            };
        }
        //Obtiene  sala de cine activa por su ID.
        public async Task<SalaCineDto?> GetSalaByIdAsync(int id)
        {
            var sala = await _salaCineRepository.GetByIdAsync(id);
            if (sala == null)
            {
                return null;
            }
            return new SalaCineDto
            {
                IdSala = sala.IdSala,
                Nombre = sala.Nombre,
                Estado = sala.Estado
            };
        }
        //Crea una nueva sala de cine
        public async Task<SalaCineDto> CreateSalaAsync(SalaCineCreateDto salaDto)
        {
            var nuevaSala = new SalaCine
            {
                Nombre = salaDto.Nombre,
                Estado = salaDto.Estado
            };

            await _salaCineRepository.AddAsync(nuevaSala);

            //Guarda en la base de datos 
            await _context.SaveChangesAsync();

            return new SalaCineDto
            {
                IdSala = nuevaSala.IdSala,
                Nombre = nuevaSala.Nombre,
                Estado = nuevaSala.Estado
            };
        }
        // Actualiza sala de cine existente
        public async Task<bool> UpdateSalaAsync(int id, SalaCineUpdateDto salaDto)
        {
            var salaExistente = await _salaCineRepository.GetByIdAsync(id);
            if (salaExistente == null)
            {
                return false;
            }
            salaExistente.Nombre = salaDto.Nombre;
            salaExistente.Estado = salaDto.Estado;

            await _salaCineRepository.UpdateAsync(salaExistente);

            //Guarda en la base de datos
            await _context.SaveChangesAsync();
            return true;
        }

        //Eliminación lógica de una sala por su ID.
        public async Task<bool> DeleteSalaLogicalAsync(int id)
        {
            var existe = await _salaCineRepository.ExistsAsync(id);
            if (!existe)
            {
                return false;
            }

            await _salaCineRepository.DeleteLogicalAsync(id);

            //Guarda cambios en la base de datos
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
