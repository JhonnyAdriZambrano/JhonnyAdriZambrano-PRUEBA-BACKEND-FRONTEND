using System.ComponentModel.DataAnnotations;

namespace API_REST_PRUEBA.DTOs
{
    public class AsignacionCreateDto
    {
        [Required(ErrorMessage = "El ID de la película es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID de película inválido.")]
        public int IdPelicula { get; set; }

        [Required(ErrorMessage = "El ID de la sala de cine es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID de sala inválido.")]
        public int IdSalaCine { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de publicación inválido.")]
        public DateTime FechaPublicacion { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha de fin inválido.")]
        public DateTime FechaFin { get; set; }
    }
}