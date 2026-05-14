using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DiscountExpiresOnFieldAddedToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Barcode",
                table: "Products",
                newName: "BarCode");

            migrationBuilder.AlterColumn<string>(
                name: "BarCode",
                table: "Products",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DiscountExpiresOn",
                table: "Products",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountExpiresOn",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "BarCode",
                table: "Products",
                newName: "Barcode");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Products",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");
        }
    }
}
