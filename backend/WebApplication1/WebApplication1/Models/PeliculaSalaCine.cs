using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_PRUEBA.Models
{
    [Table("pelicula_salacine")]
    public class PeliculaSalaCine
    {
        [Key]
        [Column("id_pelicula_salacine")]
        public int IdPeliculaSala { get; set; }

        [Required]
        [Column("id_sala_cine")]
        public int IdSalaCine { get; set; }

        [Required]
        [Column("id_pelicula")]
        public int IdPelicula { get; set; }

        [Required]
        [Column("fecha_publicacion")]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        [Column("fecha_fin")]
        public DateTime FechaFin { get; set; }

        [ForeignKey("IdSalaCine")]
        public virtual SalaCine SalaCine { get; set; }

        [ForeignKey("IdPelicula")]
        public virtual Pelicula Pelicula { get; set; }
    }
}
