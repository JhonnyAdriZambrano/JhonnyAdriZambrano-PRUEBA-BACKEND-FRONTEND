using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_PRUEBA.Models
{
    [Table("pelicula")]
    public class Pelicula
    {
        [Key]
        [Column("id_pelicula")]
        public int IdPelicula { get; set; }

        [Required]
        [StringLength(200)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("duracion")]
        public int? Duracion { get; set; }

        [Required]
        [Column("Activo")]
        public bool Activo { get; set; } = true;

        public virtual ICollection<PeliculaSalaCine> PeliculaSalaCines { get; set; } = new List<PeliculaSalaCine>();
    }
}
