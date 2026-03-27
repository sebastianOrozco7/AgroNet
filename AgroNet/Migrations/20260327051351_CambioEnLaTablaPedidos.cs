using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroNet.Migrations
{
    /// <inheritdoc />
    public partial class CambioEnLaTablaPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CantidadSolicitada",
                table: "Pedidos",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Pedidos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "IdCosecha",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdCosecha",
                table: "Pedidos",
                column: "IdCosecha");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cosechas_IdCosecha",
                table: "Pedidos",
                column: "IdCosecha",
                principalTable: "Cosechas",
                principalColumn: "CosechaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cosechas_IdCosecha",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdCosecha",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CantidadSolicitada",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "IdCosecha",
                table: "Pedidos");
        }
    }
}
