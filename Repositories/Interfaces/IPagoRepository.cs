using Cyra.Data;

namespace Cyra.Repositories.Interfaces
{
    public interface IPagoRepository
    {
        Task<Pago?> GetByIdAsync(long id);
        Task<Pago?> GetByPedidoAsync(long idPedido);
        Task<IEnumerable<Pago>> GetAllAsync();
        Task<IEnumerable<Pago>> GetByEstadoAsync(string estado);  // Cambiar a string
        Task<IEnumerable<Pago>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Pago>> GetByMetodoAsync(string metodoPago);
        Task<Pago> AddAsync(Pago pago);
        Task<Pago> UpdateAsync(Pago pago);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateEstadoAsync(long idPago, string estado);  // Cambiar a string
        Task<decimal> GetTotalRecaudadoByDateAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
