using System.ComponentModel.DataAnnotations;

namespace API_REST_PRUEBA.DTOs
{
    public class SalaCineUpdateDto
    {
        [Required(ErrorMessage = "El nombre de la sala es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres.")]
        public string? Estado { get; set; }
    }
}
