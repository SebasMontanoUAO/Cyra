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

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
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

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
