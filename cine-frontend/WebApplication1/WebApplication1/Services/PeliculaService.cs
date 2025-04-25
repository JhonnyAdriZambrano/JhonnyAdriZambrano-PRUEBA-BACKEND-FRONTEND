using API_REST_PRUEBA.Models.DTOs;
using API_REST_PRUEBA.Models;
using API_REST_PRUEBA.Repository;

namespace API_REST_PRUEBA.Services
{
    public class PeliculaService : IPeliculaService
    {
        private readonly InterfacePeliculaRepository _peliculaRepository;
        private readonly InterfacePeliculaSalaCineRepository _peliculaSalaCineRepository;

        public PeliculaService(InterfacePeliculaRepository peliculaRepository, InterfacePeliculaSalaCineRepository peliculaSalaCineRepository)
        {
            _peliculaRepository = peliculaRepository;
            _peliculaSalaCineRepository = peliculaSalaCineRepository;
        }


        public async Task<IEnumerable<PeliculaDto>> GetAllPeliculasAsync()
        {
            var peliculas = await _peliculaRepository.GetAllAsync();
            return peliculas.Select(p => new PeliculaDto { IdPelicula = p.IdPelicula, Nombre = p.Nombre, Duracion = p.Duracion }).ToList();
        }

        public async Task<PeliculaDto?> GetPeliculaByIdAsync(int id)
        {
            var pelicula = await _peliculaRepository.GetByIdAsync(id);
            if (pelicula == null) return null;
            return new PeliculaDto { IdPelicula = pelicula.IdPelicula, Nombre = pelicula.Nombre, Duracion = pelicula.Duracion };
        }

        public async Task<PeliculaDto> CreatePeliculaAsync(PeliculaCreateDto peliculaDto)
        {
            var pelicula = new Pelicula
            {
                Nombre = peliculaDto.Nombre,
                Duracion = peliculaDto.Duracion
            };
            await _peliculaRepository.AddAsync(pelicula);
            return new PeliculaDto { IdPelicula = pelicula.IdPelicula, Nombre = pelicula.Nombre, Duracion = pelicula.Duracion };
        }

        public async Task<bool> UpdatePeliculaAsync(int id, PeliculaUpdateDto peliculaDto)
        {
            var peliculaExistente = await _peliculaRepository.GetByIdAsync(id);
            if (peliculaExistente == null) return false;
            peliculaExistente.Nombre = peliculaDto.Nombre;
            peliculaExistente.Duracion = peliculaDto.Duracion;

            await _peliculaRepository.UpdateAsync(peliculaExistente);
            return true;
        }

        public async Task<bool> DeletePeliculaAsync(int id)
        {
            var existe = await _peliculaRepository.ExistsAsync(id);
            if (!existe) return false;

            await _peliculaRepository.DeleteLogicalAsync(id);
            return true;
        }

        public async Task<IEnumerable<PeliculaDto>> SearchPeliculasByNameAsync(string nombre)
        {
            var peliculas = await _peliculaRepository.GetByNameAsync(nombre);
            return peliculas.Select(p => new PeliculaDto { IdPelicula = p.IdPelicula, Nombre = p.Nombre, Duracion = p.Duracion }).ToList();
        }

        public async Task<IEnumerable<PeliculaDto>> GetPeliculasByFechaPublicacionAsync(DateTime fecha)
        {
            if (fecha.Date > DateTime.Now.AddYears(1).Date || fecha.Date < DateTime.Now.AddYears(-10).Date)
            {
                throw new ArgumentException("Fecha proporcionada no es válida o está fuera del rango permitido.");
            }

            var peliculas = await _peliculaSalaCineRepository.GetPeliculasByFechaPublicacionAsync(fecha);
            return peliculas.Select(p => new PeliculaDto { IdPelicula = p.IdPelicula, Nombre = p.Nombre, Duracion = p.Duracion }).ToList();
        }
    }

}

