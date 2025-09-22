using Cyra.Data;

namespace Cyra.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria?> GetByIdAsync(long id);
        Task<Categoria?> GetByNombreAsync(string nombre);
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<IEnumerable<Categoria>> GetActivasAsync();
        Task<Categoria> AddAsync(Categoria categoria);
        Task<Categoria> UpdateAsync(Categoria categoria);
        Task<bool> DeleteAsync(long id);
        Task<bool> ToggleActivaAsync(long id);
        Task<int> GetCountProductosAsync(long idCategoria);
        Task<bool> NombreExistsAsync(string nombre);
    }
}
