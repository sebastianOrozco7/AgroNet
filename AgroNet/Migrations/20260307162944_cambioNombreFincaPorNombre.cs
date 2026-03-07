using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroNet.Migrations
{
    /// <inheritdoc />
    public partial class cambioNombreFincaPorNombre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreFinca",
                table: "Fincas",
                newName: "Nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Fincas",
                newName: "NombreFinca");
        }
    }
}
