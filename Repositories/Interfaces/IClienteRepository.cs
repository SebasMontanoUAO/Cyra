using Cyra.Data;

namespace Cyra.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByIdAsync(long idUsuario);
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<IEnumerable<Cliente>> GetWithUsuarioAsync();
        Task<Cliente> AddAsync(Cliente cliente);
        Task<Cliente> UpdateAsync(Cliente cliente);
        Task<bool> DeleteAsync(long idUsuario);
        Task<int> GetTotalClientesAsync();
    }
}
