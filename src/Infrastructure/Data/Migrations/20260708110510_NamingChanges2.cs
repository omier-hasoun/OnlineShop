using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NamingChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventories_ProductId_Quantity",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Inventories",
                newName: "StockQuantity");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductId_Quantity",
                table: "Inventories",
                column: "ProductId")
                .Annotation("SqlServer:Include", new[] { "StockQuantity" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventories_ProductId_Quantity",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                table: "Inventories",
                newName: "Quantity");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductId_Quantity",
                table: "Inventories",
                column: "ProductId")
                .Annotation("SqlServer:Include", new[] { "Quantity" });
        }
    }
}
