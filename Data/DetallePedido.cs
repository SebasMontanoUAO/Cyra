using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Detalle_pedido", Schema = "New_schema")]
    public class DetallePedido
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Pedido")]
        public long IdPedido { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Producto")]
        public long IdProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PrecioUnitario { get; set; }

        // Navigation properties
        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }

        // Propiedad calculada
        [NotMapped]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
