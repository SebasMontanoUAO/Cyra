using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Envio", Schema = "New_schema")]
    public class Envio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdEnvio { get; set; }

        [Required]
        [StringLength(100)]
        public string DireccionEnvio { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroTelefonoReceptor { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoEnvio { get; set; } = "PENDIENTE";  // "PENDIENTE", "EMPACANDO", "EN_TRANSITO", "ENTREGADO", "CANCELADO"

        [Required]
        [ForeignKey("Pedido")]
        public long IdPedido { get; set; }

        // Navigation properties
        public virtual Pedido Pedido { get; set; }
    }
}
