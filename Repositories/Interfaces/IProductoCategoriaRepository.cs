using Cyra.Data;

namespace Cyra.Repositories.Interfaces
{
    public interface IProductoCategoriaRepository
    {
        Task<ProductoCategoria?> GetByIdAsync(long idProducto, long idCategoria);
        Task<IEnumerable<ProductoCategoria>> GetByProductoAsync(long idProducto);
        Task<IEnumerable<ProductoCategoria>> GetByCategoriaAsync(long idCategoria);
        Task<IEnumerable<ProductoCategoria>> GetAllAsync();
        Task<ProductoCategoria> AddAsync(ProductoCategoria productoCategoria);
        Task<bool> DeleteAsync(long idProducto, long idCategoria);
        Task<bool> DeleteByProductoAsync(long idProducto);
        Task<bool> DeleteByCategoriaAsync(long idCategoria);
        Task<bool> AddCategoriasToProductoAsync(long idProducto, IEnumerable<long> idsCategorias);
        Task<bool> RemoveCategoriasFromProductoAsync(long idProducto, IEnumerable<long> idsCategorias);
        Task<int> GetCountProductosByCategoriaAsync(long idCategoria);
    }
}
