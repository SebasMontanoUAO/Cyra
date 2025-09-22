using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Carrito", Schema = "New_schema")]
    public class Carrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCarrito { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey("Cliente")]
        public long IdCliente { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoCarrito { get; set; } = "ACTIVO";  // "ACTIVO", "ABANDONADO", "FINALIZADO"

        // Navigation properties
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetalleCarrito> Detalles { get; set; }
    }
}
