using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroNet.Migrations
{
    /// <inheritdoc />
    public partial class ElimineRelacionCosechaTrazabilidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidad_Cosechas_CosechaId",
                table: "Trazabilidad");

            migrationBuilder.DropIndex(
                name: "IX_Trazabilidad_CosechaId",
                table: "Trazabilidad");

            migrationBuilder.DropColumn(
                name: "CosechaId",
                table: "Trazabilidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
