using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AttachmentToValueObj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnItemRequestAttachments");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "UsersPaymentMethodsLogs",
                type: "NVARCHAR(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(3000)");

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "ReturnItemRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Specifications",
                table: "ProductVariants",
                type: "NVARCHAR(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Attributes",
                table: "Products",
                type: "NVARCHAR(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumbers",
                table: "OrderItems",
                type: "NVARCHAR(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(3000)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "ReturnItemRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "UsersPaymentMethodsLogs",
                type: "NVARCHAR(3000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Specifications",
                table: "ProductVariants",
                type: "NVARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Attributes",
                table: "Products",
                type: "NVARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumbers",
                table: "OrderItems",
                type: "NVARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReturnItemRequestAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "VARCHAR(40)", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    ReturnItemRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnItemRequestAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnItemRequestAttachments_ReturnItemRequests_ReturnItemRequestId",
                        column: x => x.ReturnItemRequestId,
                        principalTable: "ReturnItemRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnItemRequestAttachments_ReturnItemRequestId",
                table: "ReturnItemRequestAttachments",
                column: "ReturnItemRequestId");
        }
    }
}
