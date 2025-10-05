using Cyra.Data;
using Microsoft.EntityFrameworkCore;

namespace Cyra.Repositories
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly ApplicationDataContext _context;

        public VendedorRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<Vendedor> AddAsync(Vendedor vendedor)
        {
            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();
            return vendedor;
        }

        public async Task<bool> DeleteAsync(long idUsuario)
        {
            var vendedor = await _context.Vendedores.FindAsync(idUsuario);
            if (vendedor == null) return false;

            _context.Vendedores.Remove(vendedor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Vendedor>> GetAllAsync()
        {
            return await _context.Vendedores.ToListAsync();
        }

        public async Task<Vendedor?> GetByIdAsync(long idUsuario)
        {
            return await _context.Vendedores.FindAsync(idUsuario);
        }

        public async Task<Vendedor?> GetByNitAsync(string nit)
        {
            return await _context.Vendedores.FirstOrDefaultAsync(v => v.Nit == nit);
        }

        public async Task<Vendedor?> GetByNombreNegocioAsync(string nombreNegocio)
        {
            return await _context.Vendedores.FirstOrDefaultAsync(v => v.NombreNegocio == nombreNegocio);
        }

        public async Task<int> GetTotalVendedoresAsync()
        {
            return await _context.Vendedores.CountAsync();
        }

        public async Task<IEnumerable<Vendedor>> GetWithUsuarioAsync()
        {
            return await _context.Vendedores
                .Include(v => v.Usuario)
                .ToListAsync();
        }

        public async Task<bool> NitExistsAsync(string nit)
        {
            return await _context.Vendedores.AnyAsync(v => v.Nit == nit);
        }

        public async Task<Vendedor> UpdateAsync(Vendedor vendedor)
        {
            var existing = await _context.Vendedores.FindAsync(vendedor.IdUsuario);
            if (existing == null) return null;

            existing.NombreNegocio = vendedor.NombreNegocio;
            existing.Nit = vendedor.Nit;
            existing.Descripcion = vendedor.Descripcion;
            existing.Direccion = vendedor.Direccion;
            existing.NumeroEmpresarial = vendedor.NumeroEmpresarial;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
