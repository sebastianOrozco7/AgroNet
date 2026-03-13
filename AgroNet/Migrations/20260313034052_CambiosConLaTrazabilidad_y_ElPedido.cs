using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroNet.Migrations
{
    /// <inheritdoc />
    public partial class CambiosConLaTrazabilidad_y_ElPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidad_Cosechas_IdCosecha",
                table: "Trazabilidad");

            migrationBuilder.DropColumn(
                name: "EstadoPedido",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "IdCosecha",
                table: "Trazabilidad",
                newName: "IdPedido");

            migrationBuilder.RenameIndex(
                name: "IX_Trazabilidad_IdCosecha",
                table: "Trazabilidad",
                newName: "IX_Trazabilidad_IdPedido");

            migrationBuilder.AddColumn<int>(
                name: "CosechaId",
                table: "Trazabilidad",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_CosechaId",
                table: "Trazabilidad",
                column: "CosechaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trazabilidad_Cosechas_CosechaId",
                table: "Trazabilidad",
                column: "CosechaId",
                principalTable: "Cosechas",
                principalColumn: "CosechaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trazabilidad_Pedidos_IdPedido",
                table: "Trazabilidad",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "PedidoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidad_Cosechas_CosechaId",
                table: "Trazabilidad");

            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidad_Pedidos_IdPedido",
                table: "Trazabilidad");

            migrationBuilder.DropIndex(
                name: "IX_Trazabilidad_CosechaId",
                table: "Trazabilidad");

            migrationBuilder.DropColumn(
                name: "CosechaId",
                table: "Trazabilidad");

            migrationBuilder.RenameColumn(
                name: "IdPedido",
                table: "Trazabilidad",
                newName: "IdCosecha");

            migrationBuilder.RenameIndex(
                name: "IX_Trazabilidad_IdPedido",
                table: "Trazabilidad",
                newName: "IX_Trazabilidad_IdCosecha");

            migrationBuilder.AddColumn<string>(
                name: "EstadoPedido",
                table: "Pedidos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Trazabilidad_Cosechas_IdCosecha",
                table: "Trazabilidad",
                column: "IdCosecha",
                principalTable: "Cosechas",
                principalColumn: "CosechaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
