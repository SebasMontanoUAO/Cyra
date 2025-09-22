using Cyra.Data;

namespace Cyra.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido?> GetByIdAsync(long id);
        Task<Pedido?> GetByIdWithDetailsAsync(long id);
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<IEnumerable<Pedido>> GetByClienteAsync(long idCliente);
        Task<IEnumerable<Pedido>> GetByVendedorAsync(long idVendedor);
        Task<IEnumerable<Pedido>> GetByEstadoAsync(EstadoPedidoType estado);
        Task<IEnumerable<Pedido>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Pedido>> GetWithDetailsAsync();
        Task<Pedido> AddAsync(Pedido pedido);
        Task<Pedido> UpdateAsync(Pedido pedido);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateEstadoAsync(long idPedido, EstadoPedidoType nuevoEstado);
        Task<decimal> GetTotalVentasByDateAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<int> GetCountByEstadoAsync(EstadoPedidoType estado);
        Task<int> GetCountByClienteAsync(long idCliente);
    }

}
