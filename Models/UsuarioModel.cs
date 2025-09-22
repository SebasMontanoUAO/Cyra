using System.ComponentModel.DataAnnotations;

namespace Cyra.Models
{
    public class UsuarioResponseModel
    {
        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
        public string TipoUsuario { get; set; }
    }
    public class UsuarioRegisterModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }

        [Required]
        [RegularExpression("CLIENTE|VENDEDOR", ErrorMessage = "El tipo de usuario debe ser CLIENTE o VENDEDOR.")]
        public string TipoUsuario { get; set; }
    }
    public class UsuarioLoginModel
    {

    }
    public class UsuarioModel
    {

    }
}
