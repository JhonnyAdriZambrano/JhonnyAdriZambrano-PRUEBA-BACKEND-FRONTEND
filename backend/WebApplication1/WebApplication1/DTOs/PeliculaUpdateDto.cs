using System.ComponentModel.DataAnnotations;

namespace API_REST_PRUEBA.DTOs
{
    public class PeliculaUpdateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200)]
        public string Nombre { get; set; }
        [Range(1, 500, ErrorMessage = "La duración debe ser un número positivo")]
        public int? Duracion { get; set; }
    }
}
