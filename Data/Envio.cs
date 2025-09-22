using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    public enum EstadoEnvioType { PENDIENTE, EMPACANDO, EN_TRANSITO, ENTREGADO, CANCELADO }

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
        [Column(TypeName = "estado_envio_type")]
        public EstadoEnvioType EstadoEnvio { get; set; } = EstadoEnvioType.PENDIENTE;

        [Required]
        [ForeignKey("Pedido")]
        public long IdPedido { get; set; }

        // Navigation properties
        public virtual Pedido Pedido { get; set; }
    }
}
