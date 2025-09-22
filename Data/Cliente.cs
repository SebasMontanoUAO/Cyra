using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Cliente", Schema = "New_schema")]
    public class Cliente
    {
        [Key]
        [ForeignKey("Usuario")]
        public long IdUsuario { get; set; }

        public string Preferencias { get; set; }

        // Navigation properties
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Carrito> Carritos { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
