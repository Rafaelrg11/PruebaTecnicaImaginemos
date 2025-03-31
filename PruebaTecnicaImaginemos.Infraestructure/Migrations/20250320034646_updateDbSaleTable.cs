using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaImaginemos.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDbSaleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "sale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "sale",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
