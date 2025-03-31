using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaImaginemos.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class DetailSaleConfigurationNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_detail_sail_IdSale",
                table: "detail_sail",
                column: "IdSale");

            migrationBuilder.AddForeignKey(
                name: "FK_detail_sail_sale_IdSale",
                table: "detail_sail",
                column: "IdSale",
                principalTable: "sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detail_sail_sale_IdSale",
                table: "detail_sail");

            migrationBuilder.DropIndex(
                name: "IX_detail_sail_IdSale",
                table: "detail_sail");

            migrationBuilder.AddColumn<Guid>(
                name: "SaleId",
                table: "detail_sail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        }
    }
}
