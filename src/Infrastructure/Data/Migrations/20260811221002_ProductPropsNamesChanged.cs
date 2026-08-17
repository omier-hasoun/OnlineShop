using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductPropsNamesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceAfterDiscount",
                table: "Products",
                newName: "DiscountPrice");

            migrationBuilder.RenameColumn(
                name: "HasActiveDiscount",
                table: "Products",
                newName: "HasDiscount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasDiscount",
                table: "Products",
                newName: "HasActiveDiscount");

            migrationBuilder.RenameColumn(
                name: "DiscountPrice",
                table: "Products",
                newName: "PriceAfterDiscount");
        }
    }
}
