using API_REST_PRUEBA.Data;
using API_REST_PRUEBA.DTOs;
using API_REST_PRUEBA.Models;
using API_REST_PRUEBA.Repository;

namespace API_REST_PRUEBA.Services
{
    public class AsignacionService : IAsignacionService
    {
        private readonly IPeliculaSalaCineRepository _asignacionRepository;
        private readonly IPeliculaRepository _peliculaRepository;
        private readonly ISalaCineRepository _salaCineRepository;
        private readonly CineDBContext _context;

        public AsignacionService(
            IPeliculaSalaCineRepository asignacionRepository,
            IPeliculaRepository peliculaRepository,
            ISalaCineRepository salaCineRepository,
            CineDBContext context) // Inyectar DBContext
        {
            _asignacionRepository = asignacionRepository;
            _peliculaRepository = peliculaRepository;
            _salaCineRepository = salaCineRepository;
            _context = context;
        }

        public async Task<PeliculaSalaCine> CrearAsignacionAsync(AsignacionCreateDto dto)
        {

            // Validar Fechas
            if (dto.FechaFin.Date < dto.FechaPublicacion.Date)
            {
                throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de publicación.");
            }

            // Verificar existencia y estado activo de Película y Sala
            var peliculaExiste = await _peliculaRepository.ExistsAsync(dto.IdPelicula);
            var salaExiste = await _salaCineRepository.ExistsAsync(dto.IdSalaCine);

            if (!peliculaExiste)
            {
                throw new KeyNotFoundException($"La película con ID {dto.IdPelicula} no existe o está inactiva.");
            }
            if (!salaExiste)
            {
                throw new KeyNotFoundException($"La sala con ID {dto.IdSalaCine} no existe o está inactiva.");
            }

            //Solapamiento de Fechas
            bool haySolapamiento = await _asignacionRepository.CheckOverlapAsync(
                dto.IdPelicula, dto.IdSalaCine, dto.FechaPublicacion, dto.FechaFin);

            if (haySolapamiento)
            {
                throw new InvalidOperationException($"La película ya está asignada a esta sala en un período que se solapa con las fechas indicadas ({dto.FechaPublicacion.ToShortDateString()} - {dto.FechaFin.ToShortDateString()}).");
            }

            //Si todas las validaciones pasan crear la entidad
            var nuevaAsignacion = new PeliculaSalaCine
            {
                IdPelicula = dto.IdPelicula,
                IdSalaCine = dto.IdSalaCine,
                FechaPublicacion = dto.FechaPublicacion,
                FechaFin = dto.FechaFin
            };

            await _asignacionRepository.AddAsignacionAsync(nuevaAsignacion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                Console.WriteLine($"ERROR DB al guardar asignación: {dbEx}");
                throw;
            }
            return nuevaAsignacion;
        }
    }
} 
