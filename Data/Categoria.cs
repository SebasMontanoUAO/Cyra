using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Categoria", Schema = "New_schema")]
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCategoria { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        // Navigation properties
        public virtual ICollection<ProductoCategoria> Productos { get; set; }
    }
}
