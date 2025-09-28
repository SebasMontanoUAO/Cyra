using Cyra.Data;
using Cyra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Cyra.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDataContext _context;

        public ProductoRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<Producto> AddAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Producto>> GetActivosAsync()
        {
            return await _context.Productos.Where(p => p.EstadoPublicacion == "ACTIVO").ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetByCategoriaAsync(long idCategoria)
        {
            var categoria = await _context.Categorias.FindAsync(idCategoria);
            if (categoria == null) return Enumerable.Empty<Producto>();

            return await _context.Productos.Where(p => p.Categorias == categoria).ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetByEstadoAsync(string estado)
        {
            return await _context.Productos.Where(p => p.EstadoPublicacion == estado).ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(long id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<Producto?> GetByIdWithDetailsAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Producto>> GetByVendedorAsync(long idVendedor)
        {
            var vendedor = await _context.Vendedores.FindAsync(idVendedor);
            if (vendedor == null) return Enumerable.Empty<Producto>();

            return await _context.Productos.Where(p => p.Vendedor == vendedor).ToListAsync();
        }

        public async Task<int> GetCountByEstadoAsync(string estado)
        {
            var estadoPublicacion = estado.ToUpperInvariant();
            if (estadoPublicacion != "BORRADOR" || estadoPublicacion != "ACTIVO" || estadoPublicacion != "PAUSADO"
                || estadoPublicacion != "AGOTADO" || estadoPublicacion != "ELIMINADO")
            {
                throw new ArgumentException("EstadoPublicacion inválido. Use BORRADOR, ACTIVO, PAUSADO, AGOTADO, ELIMINADO");
            }

            return await _context.Productos.CountAsync(p => p.EstadoPublicacion == estadoPublicacion);
        }

        public async Task<int> GetCountByVendedorAsync(long idVendedor)
        {
            var vendedor = await _context.Vendedores.FindAsync(idVendedor);
            if (vendedor == null) return 0;

            return await _context.Productos.CountAsync(p => p.Vendedor == vendedor);
        }

        public async Task<IEnumerable<Producto>> GetDestacadosAsync(int limit = 10)
        {
            return await _context.Productos
                .Where(p => p.EstadoPublicacion == "ACTIVO")
                .OrderByDescending(p => p.FechaPublicacion) 
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetWithLowStockAsync(int stockThreshold = 5)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Producto>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task<Producto?> UpdateAsync(Producto producto)
        {
            var existing = await _context.Productos.FindAsync(producto.IdProducto);
            if (existing == null) return null;

            existing.Nombre = producto.Nombre;
            existing.Descripcion = producto.Descripcion;
            existing.Precio = producto.Precio;
            existing.Categorias = producto.Categorias;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> UpdateEstadoAsync(long idProducto, string estado)
        {
            var producto = await _context.Productos.FindAsync(idProducto);
            if (producto == null) return false;

            var estadoPublicacion = estado.ToUpperInvariant();
            if (estadoPublicacion != "BORRADOR" || estadoPublicacion != "ACTIVO" || estadoPublicacion != "PAUSADO"
                || estadoPublicacion != "AGOTADO" || estadoPublicacion != "ELIMINADO")
            {
                throw new ArgumentException("EstadoPublicacion inválido. Use BORRADOR, ACTIVO, PAUSADO, AGOTADO, ELIMINADO");
            }

            producto.EstadoPublicacion = estadoPublicacion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStockAsync(long idProducto, int nuevoStock)
        {
            var producto = await _context.Productos.FindAsync(idProducto);
            if (producto == null) return false;

            if (nuevoStock < 0) return false;

            producto.Stock = nuevoStock;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
