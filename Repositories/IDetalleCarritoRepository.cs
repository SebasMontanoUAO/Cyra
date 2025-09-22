using Cyra.Data;

namespace Cyra.Repositories
{
    public interface IDetalleCarritoRepository
    {
        Task<DetalleCarrito?> GetByIdAsync(long idCarrito, long idProducto);
        Task<IEnumerable<DetalleCarrito>> GetByCarritoAsync(long idCarrito);
        Task<IEnumerable<DetalleCarrito>> GetByProductoAsync(long idProducto);
        Task<IEnumerable<DetalleCarrito>> GetAllAsync();
        Task<DetalleCarrito> AddAsync(DetalleCarrito detalle);
        Task<DetalleCarrito> UpdateAsync(DetalleCarrito detalle);
        Task<bool> DeleteAsync(long idCarrito, long idProducto);
        Task<bool> DeleteByCarritoAsync(long idCarrito);
        Task<bool> DeleteByProductoAsync(long idProducto);
        Task<decimal> GetTotalByCarritoAsync(long idCarrito);
        Task<int> GetCantidadItemsByCarritoAsync(long idCarrito);
    }
}
