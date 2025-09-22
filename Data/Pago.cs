using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    public enum EstadoPagoType { PENDIENTE, PROCESANDO, COMPLETADO, RECHAZADO, REEMBOLSADO }

    [Table("Pagos", Schema = "New_schema")]
    public class Pago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdPago { get; set; }

        [Required]
        [StringLength(100)]
        public string MetodoPago { get; set; }

        [Required]
        [Column(TypeName = "estado_pago_type")]
        public EstadoPagoType EstadoPago { get; set; } = EstadoPagoType.PENDIENTE;

        public DateTime? FechaPago { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Monto { get; set; }

        [Required]
        [ForeignKey("Pedido")]
        public long IdPedido { get; set; }

        // Navigation properties
        public virtual Pedido Pedido { get; set; }
    }
}
