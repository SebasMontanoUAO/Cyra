using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cyra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateFixedIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "New_schema");

            migrationBuilder.CreateTable(
                name: "Categoria",
                schema: "New_schema",
                columns: table => new
                {
                    IdCategoria = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "New_schema",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TipoUsuario = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.CheckConstraint("CK_Usuario_Estado", "\"Estado\" IN ('ACTIVO', 'INACTIVO', 'SUSPENDIDO')");
                    table.CheckConstraint("CK_Usuario_TipoUsuario", "\"TipoUsuario\" IN ('CLIENTE', 'VENDEDOR')");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                schema: "New_schema",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    Preferencias = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Cliente_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "New_schema",
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendedor",
                schema: "New_schema",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    NombreNegocio = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Nit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NumeroEmpresarial = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Vendedor_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "New_schema",
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carrito",
                schema: "New_schema",
                columns: table => new
                {
                    IdCarrito = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdCliente = table.Column<long>(type: "bigint", nullable: false),
                    EstadoCarrito = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrito", x => x.IdCarrito);
                    table.CheckConstraint("CK_Carrito_EstadoCarrito", "\"EstadoCarrito\" IN ('ACTIVO', 'ABANDONADO', 'FINALIZADO')");
                    table.ForeignKey(
                        name: "FK_Carrito_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalSchema: "New_schema",
                        principalTable: "Cliente",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                schema: "New_schema",
                columns: table => new
                {
                    IdPedido = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaPedido = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    EstadoPedido = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IdCliente = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.IdPedido);
                    table.CheckConstraint("CK_Pedido_EstadoPedido", "\"EstadoPedido\" IN ('PENDIENTE', 'CONFIRMADO', 'PREPARACION', 'ENVIADO', 'ENTREGADO', 'CANCELADO')");
                    table.CheckConstraint("CK_Pedido_Total", "\"Total\" >= 0");
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalSchema: "New_schema",
                        principalTable: "Cliente",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                schema: "New_schema",
                columns: table => new
                {
                    IdProducto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    EstadoPublicacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IdVendedor = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.IdProducto);
                    table.CheckConstraint("CK_Producto_EstadoPublicacion", "\"EstadoPublicacion\" IN ('BORRADOR', 'ACTIVO', 'PAUSADO', 'AGOTADO', 'ELIMINADO')");
                    table.CheckConstraint("CK_Producto_Precio", "\"Precio\" >= 0");
                    table.CheckConstraint("CK_Producto_Stock", "\"Stock\" >= 0");
                    table.ForeignKey(
                        name: "FK_Producto_Vendedor_IdVendedor",
                        column: x => x.IdVendedor,
                        principalSchema: "New_schema",
                        principalTable: "Vendedor",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Envio",
                schema: "New_schema",
                columns: table => new
                {
                    IdEnvio = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DireccionEnvio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumeroTelefonoReceptor = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EstadoEnvio = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IdPedido = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envio", x => x.IdEnvio);
                    table.CheckConstraint("CK_Envio_EstadoEnvio", "\"EstadoEnvio\" IN ('PENDIENTE', 'EMPACANDO', 'EN_TRANSITO', 'ENTREGADO', 'CANCELADO')");
                    table.ForeignKey(
                        name: "FK_Envio_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalSchema: "New_schema",
                        principalTable: "Pedido",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                schema: "New_schema",
                columns: table => new
                {
                    IdPago = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MetodoPago = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EstadoPago = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaPago = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Monto = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    IdPedido = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.IdPago);
                    table.CheckConstraint("CK_Pago_EstadoPago", "\"EstadoPago\" IN ('PENDIENTE', 'PROCESANDO', 'COMPLETADO', 'RECHAZADO', 'REEMBOLSADO')");
                    table.CheckConstraint("CK_Pago_Monto", "\"Monto\" >= 0");
                    table.ForeignKey(
                        name: "FK_Pagos_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalSchema: "New_schema",
                        principalTable: "Pedido",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_carrito",
                schema: "New_schema",
                columns: table => new
                {
                    IdCarrito = table.Column<long>(type: "bigint", nullable: false),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_carrito", x => new { x.IdCarrito, x.IdProducto });
                    table.CheckConstraint("CK_DetalleCarrito_Cantidad", "\"Cantidad\" > 0");
                    table.CheckConstraint("CK_DetalleCarrito_Precio", "\"PrecioUnitario\" >= 0");
                    table.ForeignKey(
                        name: "FK_Detalle_carrito_Carrito_IdCarrito",
                        column: x => x.IdCarrito,
                        principalSchema: "New_schema",
                        principalTable: "Carrito",
                        principalColumn: "IdCarrito",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalle_carrito_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalSchema: "New_schema",
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_pedido",
                schema: "New_schema",
                columns: table => new
                {
                    IdPedido = table.Column<long>(type: "bigint", nullable: false),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "numeric(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_pedido", x => new { x.IdPedido, x.IdProducto });
                    table.CheckConstraint("CK_DetallePedido_Cantidad", "\"Cantidad\" > 0");
                    table.CheckConstraint("CK_DetallePedido_Precio", "\"PrecioUnitario\" >= 0");
                    table.ForeignKey(
                        name: "FK_Detalle_pedido_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalSchema: "New_schema",
                        principalTable: "Pedido",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalle_pedido_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalSchema: "New_schema",
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Imagen_producto",
                schema: "New_schema",
                columns: table => new
                {
                    IdImagen = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UrlImagen = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen_producto", x => x.IdImagen);
                    table.ForeignKey(
                        name: "FK_Imagen_producto_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalSchema: "New_schema",
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto_categoria",
                schema: "New_schema",
                columns: table => new
                {
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    IdCategoria = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto_categoria", x => new { x.IdProducto, x.IdCategoria });
                    table.ForeignKey(
                        name: "FK_Producto_categoria_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalSchema: "New_schema",
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_categoria_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalSchema: "New_schema",
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_carrito_activo",
                schema: "New_schema",
                table: "Carrito",
                columns: new[] { "IdCliente", "FechaCreacion" },
                filter: "\"EstadoCarrito\" = 'ACTIVO'");

            migrationBuilder.CreateIndex(
                name: "idx_carrito_cliente",
                schema: "New_schema",
                table: "Carrito",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_carrito_IdProducto",
                schema: "New_schema",
                table: "Detalle_carrito",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "idx_detallepedido_pedido",
                schema: "New_schema",
                table: "Detalle_pedido",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "idx_detallepedido_producto",
                schema: "New_schema",
                table: "Detalle_pedido",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "idx_envio_pedido",
                schema: "New_schema",
                table: "Envio",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "idx_imagen_producto",
                schema: "New_schema",
                table: "Imagen_producto",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "idx_pago_pedido",
                schema: "New_schema",
                table: "Pagos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "idx_pedido_cliente",
                schema: "New_schema",
                table: "Pedido",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "idx_pedido_fecha",
                schema: "New_schema",
                table: "Pedido",
                column: "FechaPedido");

            migrationBuilder.CreateIndex(
                name: "idx_producto_estado_publicacion",
                schema: "New_schema",
                table: "Producto",
                column: "EstadoPublicacion");

            migrationBuilder.CreateIndex(
                name: "idx_producto_fecha_publicacion",
                schema: "New_schema",
                table: "Producto",
                column: "FechaPublicacion");

            migrationBuilder.CreateIndex(
                name: "idx_producto_vendedor",
                schema: "New_schema",
                table: "Producto",
                column: "IdVendedor");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_categoria_IdCategoria",
                schema: "New_schema",
                table: "Producto_categoria",
                column: "IdCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalle_carrito",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Detalle_pedido",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Envio",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Imagen_producto",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Pagos",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Producto_categoria",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Carrito",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Pedido",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Categoria",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Producto",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Cliente",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Vendedor",
                schema: "New_schema");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "New_schema");
        }
    }
}
