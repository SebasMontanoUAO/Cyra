using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    public enum EstadoCarritoType { ACTIVO, ABANDONADO, FINALIZADO }

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
        [Column(TypeName = "estado_carrito_type")]
        public EstadoCarritoType EstadoCarrito { get; set; } = EstadoCarritoType.ACTIVO;

        // Navigation properties
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetalleCarrito> Detalles { get; set; }
    }
}
