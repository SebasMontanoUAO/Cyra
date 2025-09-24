using Microsoft.EntityFrameworkCore;
using Cyra.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyra.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }

        // DbSets para todas las tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
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
            base.OnModelCreating(modelBuilder);

            // Configurar enums para PostgreSQL
            // ❌ COMENTAR TEMPORALMENTE TODOS los ENUMs
            // modelBuilder.HasPostgresEnum<EstadoUsuarioType>();
            // modelBuilder.HasPostgresEnum<EstadoPublicacionType>();
            // modelBuilder.HasPostgresEnum<EstadoCarritoType>();
            // modelBuilder.HasPostgresEnum<EstadoPedidoType>();
            // modelBuilder.HasPostgresEnum<EstadoPagoType>();
            // modelBuilder.HasPostgresEnum<EstadoEnvioType>();

            // Configurar esquema por defecto
            modelBuilder.HasDefaultSchema("New_schema");

            // ✅ CONFIGURAR CLAVES COMPUESTAS PRIMERO
            ConfigureCompositeKeys(modelBuilder);

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

        // ✅ NUEVO MÉTODO PARA CONFIGURAR CLAVES COMPUESTAS
        private void ConfigureCompositeKeys(ModelBuilder modelBuilder)
        {
            // DetalleCarrito - Clave primaria compuesta (IdCarrito, IdProducto)
            modelBuilder.Entity<DetalleCarrito>()
                .HasKey(dc => new { dc.IdCarrito, dc.IdProducto });

            // DetallePedido - Clave primaria compuesta (IdPedido, IdProducto)
            modelBuilder.Entity<DetallePedido>()
                .HasKey(dp => new { dp.IdPedido, dp.IdProducto });

            // ProductoCategoria - Clave primaria compuesta (IdProducto, IdCategoria)
            modelBuilder.Entity<ProductoCategoria>()
                .HasKey(pc => new { pc.IdProducto, pc.IdCategoria });
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

            // Usuario -> Administrador (1:1)
            modelBuilder.Entity<Administrador>()
                .HasOne(a => a.Usuario)
                .WithOne(u => u.Administrador)
                .HasForeignKey<Administrador>(a => a.IdUsuario)
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

            // ✅ YA NO configurar HasKey aquí - se hace en ConfigureCompositeKeys
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

            // ✅ YA NO configurar HasKey aquí - se hace en ConfigureCompositeKeys
            // DetallePedido -> Pedido
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
            // ✅ YA NO configurar HasKey aquí - se hace en ConfigureCompositeKeys
            // ProductoCategoria -> Producto
            modelBuilder.Entity<ProductoCategoria>()
                .HasOne(pc => pc.Producto)
                .WithMany(p => p.Categorias)
                .HasForeignKey(pc => pc.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductoCategoria -> Categoria
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
                .HasDatabaseName("idx_producto_estado_publicacion");

            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.FechaPublicacion)
                .HasDatabaseName("idx_producto_fecha_publicacion");

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
                .HasDatabaseName("idx_detallepedido_pedido");

            modelBuilder.Entity<DetallePedido>()
                .HasIndex(dp => dp.IdProducto)
                .HasDatabaseName("idx_detallepedido_producto");

            // Índices para Pago
            modelBuilder.Entity<Pago>()
                .HasIndex(p => p.IdPedido)
                .HasDatabaseName("idx_pago_pedido");

            // Índices para Envio
            modelBuilder.Entity<Envio>()
                .HasIndex(e => e.IdPedido)
                .HasDatabaseName("idx_envio_pedido");

            // Índices para Carrito - CORREGIDOS
            modelBuilder.Entity<Carrito>()
                .HasIndex(c => c.IdCliente)
                .HasDatabaseName("idx_carrito_cliente");

            // ✅ CORREGIDO: Usar el nombre correcto de la columna entre comillas
            modelBuilder.Entity<Carrito>()
                .HasIndex(c => new { c.IdCliente, c.FechaCreacion })
                .HasFilter("\"EstadoCarrito\" = 'ACTIVO'")  // ← NOMBRE CORRECTO ENTRE COMILLAS
                .HasDatabaseName("idx_carrito_activo");
        }

        private void ConfigureCheckConstraints(ModelBuilder modelBuilder)
        {
            ConfigureUsuarioConstraints(modelBuilder);
            ConfigureProductoConstraints(modelBuilder);
            ConfigureCarritoConstraints(modelBuilder);
            ConfigurePedidoConstraints(modelBuilder);
            ConfigurePagoConstraints(modelBuilder);
            ConfigureEnvioConstraints(modelBuilder);
            ConfigureDetalleConstraints(modelBuilder);
        }

        private void ConfigureUsuarioConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_Usuario_TipoUsuario",
                    "\"TipoUsuario\" IN ('CLIENTE', 'VENDEDOR', 'ADMINISTRADOR')"
                ));

                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_Usuario_Estado",
                    "\"Estado\" IN ('ACTIVO', 'INACTIVO', 'SUSPENDIDO')"
                ));
            });
        }

        private void ConfigureProductoConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint("CK_Producto_Precio", "\"Precio\" >= 0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Producto_Stock", "\"Stock\" >= 0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Producto_EstadoPublicacion", "\"EstadoPublicacion\" IN ('BORRADOR', 'ACTIVO', 'PAUSADO', 'AGOTADO', 'ELIMINADO')"));
            });
        }

        private void ConfigureCarritoConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint("CK_Carrito_EstadoCarrito", "\"EstadoCarrito\" IN ('ACTIVO', 'ABANDONADO', 'FINALIZADO')"));
            });
        }

        private void ConfigurePedidoConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint("CK_Pedido_Total", "\"Total\" >= 0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Pedido_EstadoPedido", "\"EstadoPedido\" IN ('PENDIENTE', 'CONFIRMADO', 'PREPARACION', 'ENVIADO', 'ENTREGADO', 'CANCELADO')"));
            });
        }

        private void ConfigurePagoConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint("CK_Pago_Monto", "\"Monto\" >= 0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Pago_EstadoPago", "\"EstadoPago\" IN ('PENDIENTE', 'PROCESANDO', 'COMPLETADO', 'RECHAZADO', 'REEMBOLSADO')"));
            });
        }

        private void ConfigureEnvioConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Envio>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint("CK_Envio_EstadoEnvio", "\"EstadoEnvio\" IN ('PENDIENTE', 'EMPACANDO', 'EN_TRANSITO', 'ENTREGADO', 'CANCELADO')"));
            });
        }

        private void ConfigureDetalleConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleCarrito>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint("CK_DetalleCarrito_Cantidad", "\"Cantidad\" > 0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_DetalleCarrito_Precio", "\"PrecioUnitario\" >= 0"));
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.ToTable(t => t.HasCheckConstraint("CK_DetallePedido_Cantidad", "\"Cantidad\" > 0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_DetallePedido_Precio", "\"PrecioUnitario\" >= 0"));
            });
        }
    }
}