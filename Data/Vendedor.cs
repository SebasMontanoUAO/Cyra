using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Vendedor", Schema = "New_schema")]
    public class Vendedor
    {
        [Key]
        [ForeignKey("Usuario")]
        public long IdUsuario { get; set; }

        [Required]
        [StringLength(150)]
        public string NombreNegocio { get; set; }

        [Required]
        [StringLength(50)]
        public string Nit { get; set; }

        public string Descripcion { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(20)]
        public string NumeroEmpresarial { get; set; }

        // Navigation properties
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
