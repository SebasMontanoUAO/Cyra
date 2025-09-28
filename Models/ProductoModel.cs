using System.ComponentModel.DataAnnotations;

namespace Cyra.Models
{
    public class ProductoCreateModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; } = 0;

        // IdVendedor se obtiene del usuario autenticado, no se envía
        // pero cómo todavia no tenemos frontend, se va a declarar
        // este atributo se tiene que comentar una vez tengamos frontend
        [Required(ErrorMessage = "El stock es obligatorio")]
        public long IdVendedor { get; set; }

    }

    public class ProductoUpdateModel
    {
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string? Nombre { get; set; }

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Descripcion { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal? Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int? Stock { get; set; }
    }

    public class ProductoResponseModel
    {
        public long IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string EstadoPublicacion { get; set; }
        public DateTime? FechaPublicacion { get; set; }

        // Información del vendedor
        public long IdVendedor { get; set; }
        public string NombreVendedor { get; set; }

        // Colecciones relacionadas
        public List<ImagenProductoModel> Imagenes { get; set; } = new();
        public List<CategoriaModel> Categorias { get; set; } = new();
    }

    public class ProductoPublishModel
    {
        [Required(ErrorMessage = "El estado de publicación es requerido")]
        [RegularExpression("ACTIVO|PAUSADO",
            ErrorMessage = "Estado debe ser: ACTIVO o PAUSADO")]
        public string EstadoPublicacion { get; set; }
    }

    public class ImagenProductoModel
    {
        public long IdImagen { get; set; }
        public string Url { get; set; }
        public bool EsPortada { get; set; }
    }

    public class CategoriaModel
    {
        public long IdCategoria { get; set; }
        public string Nombre { get; set; }
    }
}
