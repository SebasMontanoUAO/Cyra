using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Usuario", Schema = "New_schema")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "ACTIVO";  // "ACTIVO", "INACTIVO", "SUSPENDIDO"

        [Required]
        [StringLength(20)]
        public string TipoUsuario { get; set; }  // "CLIENTE" o "VENDEDOR"

        // Navigation properties
        public virtual Vendedor Vendedor { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
