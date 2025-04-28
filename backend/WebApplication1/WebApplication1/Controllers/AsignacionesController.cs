using API_REST_PRUEBA.DTOs;
using API_REST_PRUEBA.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_PRUEBA.Controllers
{
    [ApiController]
    [Route("api/asignaciones")]
    public class AsignacionesController : Controller
    {
        private readonly IAsignacionService _asignacionService;

        public AsignacionesController(IAsignacionService asignacionService)
        {
            _asignacionService = asignacionService;
        }

        [HttpPost] //HTTP POST
        [ProducesResponseType(StatusCodes.Status201Created)]         //exito
        [ProducesResponseType(StatusCodes.Status400BadRequest)]      //error de validación DTO o lógica
        [ProducesResponseType(StatusCodes.Status404NotFound)]        //pelicula/sala no encontrada
        [ProducesResponseType(StatusCodes.Status409Conflict)]        //solapamiento de fechas
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] //error interno

        public async Task<IActionResult> CrearNuevaAsignacion([FromBody] AsignacionCreateDto asignacionDto)
        {

            try
            {
                var asignacionCreada = await _asignacionService.CrearAsignacionAsync(asignacionDto);

                return StatusCode(StatusCodes.Status201Created, asignacionCreada);

            }
            catch (ArgumentException ex) //Fechas inválidas
            {
                // 400 mensaje de error
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex) //Película o Sala no encontrada/inactiva
            {
                //404 Not Found mensaje de error
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) //Solapamiento de fechas
            {
                // 409 mensaje de error
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!!!!! [AsignacionesController] Catch final: {ex.ToString()}"); 
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocurrió un error interno inesperado al procesar la solicitud." });

            }
        }
    }
}
