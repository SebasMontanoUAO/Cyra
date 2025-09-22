using Cyra.Data;

namespace Cyra.Repositories.Interfaces
{
    public interface ICarritoRepository
    {
        Task<Carrito?> GetByIdAsync(long id);
        Task<Carrito?> GetByIdWithDetailsAsync(long id);
        Task<Carrito?> GetActiveByClienteAsync(long idCliente);
        Task<IEnumerable<Carrito>> GetByClienteAsync(long idCliente);
        Task<IEnumerable<Carrito>> GetByEstadoAsync(EstadoCarritoType estado);
        Task<IEnumerable<Carrito>> GetAbandonadosAsync(DateTime desdeFecha);
        Task<Carrito> AddAsync(Carrito carrito);
        Task<Carrito> UpdateAsync(Carrito carrito);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateEstadoAsync(long idCarrito, EstadoCarritoType nuevoEstado);
        Task<bool> CleanOldAbandonedCartsAsync(int daysOld);
    }
}
