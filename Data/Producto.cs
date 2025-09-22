using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Producto", Schema = "New_schema")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; } = 0;

        [Required]
        [StringLength(20)]
        public string EstadoPublicacion { get; set; } = "BORRADOR";  // "BORRADOR", "ACTIVO", "PAUSADO", "AGOTADO", "ELIMINADO"

        public DateTime? FechaPublicacion { get; set; }

        [Required]
        [ForeignKey("Vendedor")]
        public long IdVendedor { get; set; }

        // Navigation properties
        public virtual Vendedor Vendedor { get; set; }
        public virtual ICollection<ImagenProducto> Imagenes { get; set; }
        public virtual ICollection<DetalleCarrito> DetallesCarrito { get; set; }
        public virtual ICollection<DetallePedido> DetallesPedido { get; set; }
        public virtual ICollection<ProductoCategoria> Categorias { get; set; }
    }
}
