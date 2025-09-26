using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Administrador", Schema = "New_schema")]
    public class Administrador
    {
        [Key]
        [ForeignKey("Usuario")]
        public long IdUsuario { get; set; }

        [Required]
        [StringLength(150)]
        public string Cargo { get; set; }   // Ej: "SuperAdmin", "Soporte", etc.

        [StringLength(200)]
        public string Departamento { get; set; }

        // Navigation property
        public virtual Usuario Usuario { get; set; }
    }
}