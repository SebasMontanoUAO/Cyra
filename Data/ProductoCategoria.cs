using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Producto_categoria", Schema = "New_schema")]
    public class ProductoCategoria
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Producto")]
        public long IdProducto { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Categoria")]
        public long IdCategoria { get; set; }

        // Navigation properties
        public virtual Producto Producto { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
