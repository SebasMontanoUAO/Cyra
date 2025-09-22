using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Detalle_carrito", Schema = "New_schema")]
    public class DetalleCarrito
    {
        // ❌ NO usar [Key] aquí - se configura en DbContext
        [ForeignKey("Carrito")]
        public long IdCarrito { get; set; }

        [ForeignKey("Producto")]
        public long IdProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PrecioUnitario { get; set; }

        // Navigation properties
        public virtual Carrito Carrito { get; set; }
        public virtual Producto Producto { get; set; }

        // Propiedad calculada (no se mapea a la BD)
        [NotMapped]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
