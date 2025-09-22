using Cyra.Data;

namespace Cyra.Repositories
{
    public interface IImagenProductoRepository
    {
        Task<ImagenProducto?> GetByIdAsync(long id);
        Task<IEnumerable<ImagenProducto>> GetByProductoAsync(long idProducto);
        Task<IEnumerable<ImagenProducto>> GetAllAsync();
        Task<ImagenProducto?> GetImagenPrincipalAsync(long idProducto);
        Task<ImagenProducto> AddAsync(ImagenProducto imagen);
        Task<ImagenProducto> UpdateAsync(ImagenProducto imagen);
        Task<bool> DeleteAsync(long id);
        Task<bool> DeleteByProductoAsync(long idProducto);
        Task<bool> SetImagenPrincipalAsync(long idImagen);
        Task<int> GetCountByProductoAsync(long idProducto);
        Task<bool> ReordenarImagenesAsync(long idProducto, Dictionary<long, int> nuevosOrdenes);
    }
}
