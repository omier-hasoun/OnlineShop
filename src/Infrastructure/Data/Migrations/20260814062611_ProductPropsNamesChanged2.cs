using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductPropsNamesChanged2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroups_Products_FeaturedProductId",
                table: "ProductGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_Search",
                table: "ProductGroups");

            migrationBuilder.DropIndex(
                name: "UX_ProductGroup_FeaturedProductId",
                table: "ProductGroups");

            migrationBuilder.RenameColumn(
                name: "FeaturedProductId",
                table: "ProductGroups",
                newName: "MainProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "ProductGroups",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(9,4)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_Search",
                table: "ProductGroups",
                columns: new[] { "Status", "NormalizedTitle" },
                filter: "[MainProductId] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "Id", "MainProductId", "Title", "BrandName", "AverageRating" });

            migrationBuilder.CreateIndex(
                name: "UX_ProductGroup_FeaturedProductId",
                table: "ProductGroups",
                column: "MainProductId",
                unique: true,
                filter: "[MainProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroups_Products_MainProductId",
                table: "ProductGroups",
                column: "MainProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroups_Products_MainProductId",
                table: "ProductGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_Search",
                table: "ProductGroups");

            migrationBuilder.DropIndex(
                name: "UX_ProductGroup_FeaturedProductId",
                table: "ProductGroups");

            migrationBuilder.RenameColumn(
                name: "MainProductId",
                table: "ProductGroups",
                newName: "FeaturedProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageRating",
                table: "ProductGroups",
                type: "DECIMAL(9,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_Search",
                table: "ProductGroups",
                columns: new[] { "Status", "NormalizedTitle" },
                filter: "[FeaturedProductId] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "Id", "FeaturedProductId", "Title", "BrandName" });

            migrationBuilder.CreateIndex(
                name: "UX_ProductGroup_FeaturedProductId",
                table: "ProductGroups",
                column: "FeaturedProductId",
                unique: true,
                filter: "[FeaturedProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroups_Products_FeaturedProductId",
                table: "ProductGroups",
                column: "FeaturedProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
