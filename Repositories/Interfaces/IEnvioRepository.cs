using Cyra.Data;

namespace Cyra.Repositories.Interfaces
{
    public interface IEnvioRepository
    {
        Task<Envio?> GetByIdAsync(long id);
        Task<Envio?> GetByPedidoAsync(long idPedido);
        Task<IEnumerable<Envio>> GetAllAsync();
        Task<IEnumerable<Envio>> GetByEstadoAsync(EstadoEnvioType estado);
        Task<IEnumerable<Envio>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<Envio> AddAsync(Envio envio);
        Task<Envio> UpdateAsync(Envio envio);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateEstadoAsync(long idEnvio, EstadoEnvioType nuevoEstado);
        Task<int> GetCountByEstadoAsync(EstadoEnvioType estado);
    }
}
