using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaImaginemos.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "sale",
                newName: "TimeSale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeSale",
                table: "sale",
                newName: "DateTime");
        }
    }
}
