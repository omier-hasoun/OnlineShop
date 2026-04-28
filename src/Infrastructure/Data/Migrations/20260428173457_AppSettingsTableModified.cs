using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppSettingsTableModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CartItems_Quantity",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "Key",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "AppSettings");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AppSettings",
                newName: "Key");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "NVARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSettings",
                table: "AppSettings",
                column: "Key")
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSettings",
                table: "AppSettings");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "AppSettings",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "NVARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppSettings",
                type: "NVARCHAR(1000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "AppSettings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "Key",
                table: "AppSettings",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CartItems_Quantity",
                table: "CartItems",
                sql: "[Quantity] between 1 and 1000");
        }
    }
}
