using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Imagen_producto", Schema = "New_schema")]
    public class ImagenProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdImagen { get; set; }

        [Required]
        [StringLength(300)]
        public string UrlImagen { get; set; }

        [Required]
        public int Orden { get; set; } = 1;

        [Required]
        [ForeignKey("Producto")]
        public long IdProducto { get; set; }

        // Navigation properties
        public virtual Producto Producto { get; set; }
    }
}
