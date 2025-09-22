using Cyra.Data;

namespace Cyra.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(long id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<IEnumerable<Usuario>> GetByTipoAsync(string tipoUsuario);
        Task<IEnumerable<Usuario>> GetByEstadoAsync(EstadoUsuarioType estado);
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(long id);
        Task<bool> EmailExistsAsync(string email);
        Task<int> GetCountByTipoAsync(string tipoUsuario);
    }
}
