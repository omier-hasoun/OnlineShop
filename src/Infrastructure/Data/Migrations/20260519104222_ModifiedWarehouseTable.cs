using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedWarehouseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Addresses_AddressId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CountryCode",
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

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Warehouses",
                type: "CHAR(2)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Addresses_AddressId",
                table: "Warehouses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
