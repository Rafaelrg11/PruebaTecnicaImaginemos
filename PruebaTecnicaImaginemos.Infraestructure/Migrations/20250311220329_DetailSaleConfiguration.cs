using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaImaginemos.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class DetailSaleConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detail_sail_products_IdProduct",
                table: "detail_sail");

            migrationBuilder.DropForeignKey(
                name: "FK_detail_sail_sale_IdSale",
                table: "detail_sail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detail_sail",
                table: "detail_sail");

            migrationBuilder.RenameTable(
                name: "detail_sail",
                newName: "detail_sale");

            migrationBuilder.RenameIndex(
                name: "IX_detail_sail_IdSale",
                table: "detail_sale",
                newName: "IX_detail_sale_IdSale");

            migrationBuilder.RenameIndex(
                name: "IX_detail_sail_IdProduct",
                table: "detail_sale",
                newName: "IX_detail_sale_IdProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detail_sale",
                table: "detail_sale",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_detail_sale_products_IdProduct",
                table: "detail_sale",
                column: "IdProduct",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detail_sale_sale_IdSale",
                table: "detail_sale",
                column: "IdSale",
                principalTable: "sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detail_sale_products_IdProduct",
                table: "detail_sale");

            migrationBuilder.DropForeignKey(
                name: "FK_detail_sale_sale_IdSale",
                table: "detail_sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detail_sale",
                table: "detail_sale");

            migrationBuilder.RenameTable(
                name: "detail_sale",
                newName: "detail_sail");

            migrationBuilder.RenameIndex(
                name: "IX_detail_sale_IdSale",
                table: "detail_sail",
                newName: "IX_detail_sail_IdSale");

            migrationBuilder.RenameIndex(
                name: "IX_detail_sale_IdProduct",
                table: "detail_sail",
                newName: "IX_detail_sail_IdProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detail_sail",
                table: "detail_sail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_detail_sail_products_IdProduct",
                table: "detail_sail",
                column: "IdProduct",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detail_sail_sale_IdSale",
                table: "detail_sail",
                column: "IdSale",
                principalTable: "sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
