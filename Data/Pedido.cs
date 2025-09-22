using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cyra.Data
{
    public enum EstadoPedidoType { PENDIENTE, CONFIRMADO, PREPARACION, ENVIADO, ENTREGADO, CANCELADO }

    [Table("Pedido", Schema = "New_schema")]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdPedido { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Total { get; set; }

        [Required]
        [Column(TypeName = "estado_pedido_type")]
        public EstadoPedidoType EstadoPedido { get; set; } = EstadoPedidoType.PENDIENTE;

        [Required]
        [ForeignKey("Cliente")]
        public long IdCliente { get; set; }

        // Navigation properties
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
    }
}
