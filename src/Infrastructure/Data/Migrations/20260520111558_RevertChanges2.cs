using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RevertChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Addresses_AddressId",
                table: "Warehouses");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Addresses_AddressId",
                table: "Warehouses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Addresses_AddressId",
                table: "Warehouses");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Addresses_AddressId",
                table: "Warehouses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
