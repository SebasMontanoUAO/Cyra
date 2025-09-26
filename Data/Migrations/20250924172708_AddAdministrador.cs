using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Usuario_TipoUsuario",
                schema: "New_schema",
                table: "Usuario");

            migrationBuilder.CreateTable(
                name: "Administrador",
                schema: "New_schema",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    Cargo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Departamento = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Administrador_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "New_schema",
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Usuario_TipoUsuario",
                schema: "New_schema",
                table: "Usuario",
                sql: "\"TipoUsuario\" IN ('CLIENTE', 'VENDEDOR', 'ADMINISTRADOR')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador",
                schema: "New_schema");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Usuario_TipoUsuario",
                schema: "New_schema",
                table: "Usuario");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Usuario_TipoUsuario",
                schema: "New_schema",
                table: "Usuario",
                sql: "\"TipoUsuario\" IN ('CLIENTE', 'VENDEDOR')");
        }
    }
}
