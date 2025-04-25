using API_REST_PRUEBA.Models.DTOs;
using API_REST_PRUEBA.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_PRUEBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
            private readonly IPeliculaService _peliculaService;

            public PeliculaController(IPeliculaService peliculaService)
            {
                _peliculaService = peliculaService;
            }

            [HttpGet]
            [ProducesResponseType(typeof(IEnumerable<PeliculaDto>), 200)]
            public async Task<IActionResult> GetAllPeliculas()
            {
            var peliculas = await _peliculaService.GetAllPeliculasAsync();
                return Ok(peliculas);
            }

            [HttpGet("{id:int}", Name = "GetPeliculaById")]
            [ProducesResponseType(typeof(PeliculaDto), 200)]
            [ProducesResponseType(404)]
            public async Task<IActionResult> GetPelicula(int id)
            {
                var pelicula = await _peliculaService.GetPeliculaByIdAsync(id);
                if (pelicula == null)
                {
                    return NotFound();
                }
                return Ok(pelicula);
            }

            [HttpGet("buscarPorNombre")]
            [ProducesResponseType(typeof(IEnumerable<PeliculaDto>), 200)]
            public async Task<IActionResult> BuscarPeliculasPorNombre([FromQuery] string nombre)
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return BadRequest("El parámetro 'nombre' es requerido.");
                }
                var peliculas = await _peliculaService.SearchPeliculasByNameAsync(nombre);
                return Ok(peliculas);
            }

            [HttpGet("porFechaPublicacion")]
            [ProducesResponseType(typeof(IEnumerable<PeliculaDto>), 200)]
            [ProducesResponseType(400)]
            public async Task<IActionResult> GetPeliculasPorFecha([FromQuery] DateTime fecha)
            {
                try
                {
                    var peliculas = await _peliculaService.GetPeliculasByFechaPublicacionAsync(fecha);
                    return Ok(peliculas);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Ocurrió un error interno al procesar la solicitud.");
                }
            }

            [HttpPost]
            [ProducesResponseType(typeof(PeliculaDto), 201)]
            [ProducesResponseType(400)]
            public async Task<IActionResult> CrearPelicula([FromBody] PeliculaCreateDto peliculaDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    var peliculaCreada = await _peliculaService.CreatePeliculaAsync(peliculaDto);
                    return CreatedAtRoute("GetPeliculaById", new { id = peliculaCreada.IdPelicula }, peliculaCreada);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Ocurrió un error interno al crear la película.");
                }
            }

            [HttpPut("{id:int}")]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            [ProducesResponseType(404)]
            public async Task<IActionResult> ActualizarPelicula(int id, [FromBody] PeliculaUpdateDto peliculaDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    var actualizado = await _peliculaService.UpdatePeliculaAsync(id, peliculaDto);
                    if (!actualizado)
                    {
                        return NotFound($"No se encontró la película con ID {id}.");
                    }
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Ocurrió un error interno al actualizar la película.");
                }
            }

            [HttpDelete("{id:int}")]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public async Task<IActionResult> BorrarPelicula(int id)
            {
                try
                {
                    var borrado = await _peliculaService.DeletePeliculaAsync(id);
                    if (!borrado)
                    {
                        return NotFound($"No se encontró la película con ID {id}.");
                    }
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Ocurrió un error interno al borrar la película.");
                }
            }
        }
    }
