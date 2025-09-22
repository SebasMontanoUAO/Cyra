using Cyra.Data;

namespace Cyra.Repositories
{
    public interface IVendedorRepository
    {
        Task<Vendedor?> GetByIdAsync(long idUsuario);
        Task<Vendedor?> GetByNitAsync(string nit);
        Task<Vendedor?> GetByNombreNegocioAsync(string nombreNegocio);
        Task<IEnumerable<Vendedor>> GetAllAsync();
        Task<IEnumerable<Vendedor>> GetWithUsuarioAsync();
        Task<Vendedor> AddAsync(Vendedor vendedor);
        Task<Vendedor> UpdateAsync(Vendedor vendedor);
        Task<bool> DeleteAsync(long idUsuario);
        Task<bool> NitExistsAsync(string nit);
        Task<int> GetTotalVendedoresAsync();
    }
}
