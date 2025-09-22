using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key, Required]
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
    }
}
