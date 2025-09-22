using Cyra.Data;
using Cyra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cyra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDataContext _context;

        public UsuarioRepository(ApplicationDataContext context) { _context = context; }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(long id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Usuario>> GetByTipoAsync(string tipoUsuario)
        {
            var tipo = tipoUsuario.ToUpperInvariant();
            if (tipo != "CLIENTE" && tipo != "VENDEDOR")
            {
                throw new ArgumentException("TipoUsuario inválido. Use CLIENTE o VENDEDOR.");
            }

            return await _context.Usuarios
                .Where(u => u.TipoUsuario == tipo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetByEstadoAsync(EstadoUsuarioType estado)
        {
            return await _context.Usuarios
                .Where(u => u.Estado == estado)
                .ToListAsync();
        }


        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> UpdateAsync(Usuario usuario)
        {
            var existing = await _context.Usuarios.FindAsync(usuario.IdUsuario);
            if (existing == null) return null;

            existing.Nombre = usuario.Nombre;
            existing.Telefono = usuario.Telefono;
            existing.Direccion = usuario.Direccion;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email no puede estar vacío.", nameof(email));

            var normalizedEmail = email.ToLowerInvariant().Trim();

            return await _context.Usuarios
                .AnyAsync(u => u.Email.ToLower() == normalizedEmail);
        }

        public async Task<int> GetCountByTipoAsync(string tipoUsuario)
        {
            var tipo = tipoUsuario.ToUpperInvariant();
            if (tipo != "CLIENTE" && tipo != "VENDEDOR")
            {
                throw new ArgumentException("TipoUsuario inválido. Use CLIENTE o VENDEDOR.");
            }

            return await _context.Usuarios
                .CountAsync(u => u.TipoUsuario == tipo);
        }
    }
}
