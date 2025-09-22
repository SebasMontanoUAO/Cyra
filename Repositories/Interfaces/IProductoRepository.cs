using Cyra.Data;

namespace Cyra.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<Producto?> GetByIdAsync(long id);
        Task<Producto?> GetByIdWithDetailsAsync(long id);
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<IEnumerable<Producto>> GetByVendedorAsync(long idVendedor);
        Task<IEnumerable<Producto>> GetByEstadoAsync(string estado);  // Cambiar a string
        Task<IEnumerable<Producto>> GetByCategoriaAsync(long idCategoria);
        Task<IEnumerable<Producto>> GetActivosAsync();
        Task<IEnumerable<Producto>> SearchAsync(string searchTerm);
        Task<IEnumerable<Producto>> GetWithLowStockAsync(int stockThreshold = 5);
        Task<IEnumerable<Producto>> GetDestacadosAsync(int limit = 10);
        Task<Producto> AddAsync(Producto producto);
        Task<Producto> UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateStockAsync(long idProducto, int nuevoStock);
        Task<bool> UpdateEstadoAsync(long idProducto, string estado);  // Cambiar a string
        Task<int> GetCountByVendedorAsync(long idVendedor);
        Task<int> GetCountByEstadoAsync(string estado);  // Cambiar a string
    }
}
