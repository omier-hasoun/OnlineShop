using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_Search",
                table: "ProductGroups");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_Search",
                table: "ProductGroups",
                columns: new[] { "Status", "NormalizedTitle" },
                filter: "[FeaturedProductId] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "Id", "FeaturedProductId", "Title", "BrandName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_Search",
                table: "ProductGroups");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_Search",
                table: "ProductGroups",
                columns: new[] { "Status", "NormalizedTitle" },
                filter: "[FeaturedProductId] IS NOT NULL");
        }
    }
}
