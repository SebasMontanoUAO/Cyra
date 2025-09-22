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
        Task<IEnumerable<Pedido>> GetByEstadoAsync(string estado);  // Cambiar a string
        Task<IEnumerable<Pedido>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Pedido>> GetWithDetailsAsync();
        Task<Pedido> AddAsync(Pedido pedido);
        Task<Pedido> UpdateAsync(Pedido pedido);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateEstadoAsync(long idPedido, string estado);  // Cambiar a string
        Task<decimal> GetTotalVentasByDateAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<int> GetCountByEstadoAsync(string estado);  // Cambiar a string
        Task<int> GetCountByClienteAsync(long idCliente);
    }

}
