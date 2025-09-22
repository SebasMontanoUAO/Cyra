using Cyra.Data;

namespace Cyra.Repositories.Interfaces
{
    public interface IDetallePedidoRepository
    {
        Task<DetallePedido?> GetByIdAsync(long idPedido, long idProducto);
        Task<IEnumerable<DetallePedido>> GetByPedidoAsync(long idPedido);
        Task<IEnumerable<DetallePedido>> GetByProductoAsync(long idProducto);
        Task<IEnumerable<DetallePedido>> GetAllAsync();
        Task<DetallePedido> AddAsync(DetallePedido detalle);
        Task<DetallePedido> UpdateAsync(DetallePedido detalle);
        Task<bool> DeleteAsync(long idPedido, long idProducto);
        Task<bool> DeleteByPedidoAsync(long idPedido);
        Task<decimal> GetTotalByPedidoAsync(long idPedido);
        Task<int> GetCantidadProductosVendidosAsync(long idProducto);
    }
}
