using API_REST_PRUEBA.DTOs;
using API_REST_PRUEBA.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_PRUEBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly ISalaCineService _salaCineService;

        public SalasController(ISalaCineService salaCineService)
        {
            _salaCineService = salaCineService;
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
    }
}
