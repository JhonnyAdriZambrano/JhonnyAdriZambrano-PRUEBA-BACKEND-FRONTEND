using API_REST_PRUEBA.DTOs;
using API_REST_PRUEBA.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_REST_PRUEBA.Controllers
{

    [ApiController]
    [Route("api/salas")]
    public class SalasController : ControllerBase
    {
        private readonly ISalaCineService _salaCineService;

        public SalasController(ISalaCineService salaCineService)
        {
            _salaCineService = salaCineService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SalaCineDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearSala([FromBody] SalaCineCreateDto salaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var salaCreada = await _salaCineService.CreateSalaAsync(salaDto);

                return StatusCode(StatusCodes.Status201Created, salaCreada);

            }
            catch (DbUpdateException dbEx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al guardar en la base de datos." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno al crear la sala." });
            }
        }

        [HttpGet] //Peticiones GET a la ruta base /api/salas
        [ProducesResponseType(typeof(IEnumerable<SalaCineDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSalas()
        {
            try
            {
                var salas = await _salaCineService.GetAllSalasAsync();
                return Ok(salas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al obtener la lista de salas.");
            }
        }

        [HttpGet("statusPorNombre")]
        [ProducesResponseType(typeof(SalaStatusDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStatusPorNombre([FromQuery] string nombreSala)
        {
            if (string.IsNullOrWhiteSpace(nombreSala))
            {
                return BadRequest("El parámetro 'nombreSala' es requerido.");
            }
            try
            {
                var status = await _salaCineService.GetSalaStatusByNameAsync(nombreSala);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error interno al obtener el estado de la sala: {ex.Message}");
            }
        }

        //Eliminación lógica
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]      //Éxito
        [ProducesResponseType(StatusCodes.Status404NotFound)]        //No encontrado
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] //Error interno
        public async Task<IActionResult> BorrarSala(int id)
        {
            try
            {
                //borrado logico
                var borradoExitoso = await _salaCineService.DeleteSalaLogicalAsync(id);

                if (borradoExitoso)
                {
                    return NoContent(); //HTTP 204
                }
                else
                {
                    return NotFound(new { message = $"No se encontró la sala con ID {id} para eliminar." }); // HTTP 404
                }
            }
            catch (Exception ex)
            {
                //saber de erroes
                Console.WriteLine($"Error inesperado en BorrarSala (ID: {id}): {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocurrió un error interno al intentar eliminar la sala." });
            }
        }

        [HttpGet("stats")] // /api/salas/stats
        [ProducesResponseType(typeof(DashboardStatsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardEstadisticas()
        {
            try
            {
                var stats = await _salaCineService.GetDashboardStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error obteniendo estadísticas del dashboard.");
            }
        }
    }
}
