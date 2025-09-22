using Cyra.Models;
using Microsoft.EntityFrameworkCore;

namespace Cyra.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options): base(options) { }
        // DbSets para todas las tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ImagenProducto> ImagenesProducto { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetalleCarrito> DetallesCarrito { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<ProductoCategoria> ProductosCategorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar enums para PostgreSQL
            modelBuilder.HasPostgresEnum<EstadoUsuarioType>();
            modelBuilder.HasPostgresEnum<EstadoPublicacionType>();
            modelBuilder.HasPostgresEnum<EstadoCarritoType>();
            modelBuilder.HasPostgresEnum<EstadoPedidoType>();
            modelBuilder.HasPostgresEnum<EstadoPagoType>();
            modelBuilder.HasPostgresEnum<EstadoEnvioType>();

            // Configurar esquema por defecto
            modelBuilder.HasDefaultSchema("New_schema");

            // Configurar todas las relaciones
            ConfigureUsuarioRelations(modelBuilder);
            ConfigureProductoRelations(modelBuilder);
            ConfigureCarritoRelations(modelBuilder);
            ConfigurePedidoRelations(modelBuilder);
            ConfigureCategoriaRelations(modelBuilder);

            // Configurar índices para todas las tablas
            ConfigureIndexes(modelBuilder);

            // Configurar constraints de CHECK
            ConfigureCheckConstraints(modelBuilder);
        }

        private void ConfigureUsuarioRelations(ModelBuilder modelBuilder)
        {
            // Usuario -> Vendedor (1:1)
            modelBuilder.Entity<Vendedor>()
                .HasOne(v => v.Usuario)
                .WithOne(u => u.Vendedor)
                .HasForeignKey<Vendedor>(v => v.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Usuario -> Cliente (1:1)
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureProductoRelations(ModelBuilder modelBuilder)
        {
            // Producto -> Vendedor
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Vendedor)
                .WithMany(v => v.Productos)
                .HasForeignKey(p => p.IdVendedor)
                .OnDelete(DeleteBehavior.Cascade);

            // Producto -> Imágenes (1:N)
            modelBuilder.Entity<ImagenProducto>()
                .HasOne(ip => ip.Producto)
                .WithMany(p => p.Imagenes)
                .HasForeignKey(ip => ip.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureCarritoRelations(ModelBuilder modelBuilder)
        {
            // Carrito -> Cliente
            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Cliente)
                .WithMany(cl => cl.Carritos)
                .HasForeignKey(c => c.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            // DetalleCarrito -> Carrito
            modelBuilder.Entity<DetalleCarrito>()
                .HasOne(dc => dc.Carrito)
                .WithMany(c => c.Detalles)
                .HasForeignKey(dc => dc.IdCarrito)
                .OnDelete(DeleteBehavior.Cascade);

            // DetalleCarrito -> Producto
            modelBuilder.Entity<DetalleCarrito>()
                .HasOne(dc => dc.Producto)
                .WithMany(p => p.DetallesCarrito)
                .HasForeignKey(dc => dc.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigurePedidoRelations(ModelBuilder modelBuilder)
        {
            // Pedido -> Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            // DetallePedido -> Pedido (Composite Key)
            modelBuilder.Entity<DetallePedido>()
                .HasKey(dp => new { dp.IdPedido, dp.IdProducto });

            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(dp => dp.IdPedido)
                .OnDelete(DeleteBehavior.Restrict);

            // DetallePedido -> Producto
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Producto)
                .WithMany(p => p.DetallesPedido)
                .HasForeignKey(dp => dp.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // Pago -> Pedido
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Pedido)
                .WithMany(ped => ped.Pagos)
                .HasForeignKey(p => p.IdPedido)
                .OnDelete(DeleteBehavior.Restrict);

            // Envio -> Pedido
            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Pedido)
                .WithMany(p => p.Envios)
                .HasForeignKey(e => e.IdPedido)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureCategoriaRelations(ModelBuilder modelBuilder)
        {
            // ProductoCategoria (Many-to-Many)
            modelBuilder.Entity<ProductoCategoria>()
                .HasKey(pc => new { pc.IdProducto, pc.IdCategoria });

            modelBuilder.Entity<ProductoCategoria>()
                .HasOne(pc => pc.Producto)
                .WithMany(p => p.Categorias)
                .HasForeignKey(pc => pc.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductoCategoria>()
                .HasOne(pc => pc.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(pc => pc.IdCategoria)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            // Índices para Producto
            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.IdVendedor)
                .HasDatabaseName("idx_producto_vendedor");

            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.EstadoPublicacion)
                .HasDatabaseName("idx_estado_ubicacion");

            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.FechaPublicacion)
                .HasDatabaseName("idx_producto_fecha_publicacion");

            // Índices para búsqueda full-text (PostgreSQL específico)
            modelBuilder.Entity<Producto>()
                .HasIndex(p => new { })
                .HasMethod("GIN")
                .HasOperators("gin_trgm_ops")
                .HasFilter("estado_publicacion = 'ACTIVO'")
                .HasDatabaseName("idx_producto_busqueda_texto");

            // Índices para ImagenProducto
            modelBuilder.Entity<ImagenProducto>()
                .HasIndex(ip => ip.IdProducto)
                .HasDatabaseName("idx_imagen_producto");

            // Índices para Pedido
            modelBuilder.Entity<Pedido>()
                .HasIndex(p => p.IdCliente)
                .HasDatabaseName("idx_pedido_cliente");

            modelBuilder.Entity<Pedido>()
                .HasIndex(p => p.FechaPedido)
                .HasDatabaseName("idx_pedido_fecha");

            // Índices para DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasIndex(dp => dp.IdPedido)
                .HasDatabaseName("idx_pedido");

            modelBuilder.Entity<DetallePedido>()
                .HasIndex(dp => dp.IdProducto)
                .HasDatabaseName("idx_producto_pedido");

            // Índices para Pago
            modelBuilder.Entity<Pago>()
                .HasIndex(p => p.IdPedido)
                .HasDatabaseName("idx_pago_pedido");

            // Índices para Envio
            modelBuilder.Entity<Envio>()
                .HasIndex(e => e.IdPedido)
                .HasDatabaseName("idx_envio_pedido");

            // Índices para Carrito
            modelBuilder.Entity<Carrito>()
                .HasIndex(c => c.IdCliente)
                .HasDatabaseName("idx_carrito_cliente");

            modelBuilder.Entity<Carrito>()
                .HasIndex(c => new { c.IdCliente, c.FechaCreacion })
                .HasFilter("estado_carrito = 'ACTIVO'")
                .HasDatabaseName("idx_carrito_activo");
        }

        private void ConfigureCheckConstraints(ModelBuilder modelBuilder)
        {
            // Constraints para Usuario
            modelBuilder.Entity<Usuario>()
                .HasCheckConstraint("CK_Usuario_TipoUsuario", "tipo_usuario IN ('CLIENTE', 'VENDEDOR')");

            // Constraints para Producto
            modelBuilder.Entity<Producto>()
                .HasCheckConstraint("CK_Producto_Precio", "precio >= 0");

            modelBuilder.Entity<Producto>()
                .HasCheckConstraint("CK_Producto_Stock", "stock >= 0");

            // Constraints para DetalleCarrito y DetallePedido
            modelBuilder.Entity<DetalleCarrito>()
                .HasCheckConstraint("CK_DetalleCarrito_Cantidad", "cantidad > 0");

            modelBuilder.Entity<DetalleCarrito>()
                .HasCheckConstraint("CK_DetalleCarrito_Precio", "precio_unitario >= 0");

            modelBuilder.Entity<DetallePedido>()
                .HasCheckConstraint("CK_DetallePedido_Cantidad", "cantidad > 0");

            modelBuilder.Entity<DetallePedido>()
                .HasCheckConstraint("CK_DetallePedido_Precio", "precio_unitario >= 0");

            // Constraints para Pedido y Pago
            modelBuilder.Entity<Pedido>()
                .HasCheckConstraint("CK_Pedido_Total", "total >= 0");

            modelBuilder.Entity<Pago>()
                .HasCheckConstraint("CK_Pago_Monto", "monto >= 0");
        }
    }
}