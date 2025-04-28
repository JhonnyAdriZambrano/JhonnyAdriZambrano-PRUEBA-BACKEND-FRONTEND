using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_REST_PRUEBA.Models
{
    [Table("sala_cine")]
    public class SalaCine
    {
        [Key]
        [Column("id_sala")]
        public int IdSala { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Column("estado")]
        public string? Estado { get; set; }

        [Required]
        [Column("Activo")]
        public bool Activo { get; set; } = true;

        public virtual ICollection<PeliculaSalaCine> PeliculaSalaCines { get; set; } = new List<PeliculaSalaCine>();
    }
}
