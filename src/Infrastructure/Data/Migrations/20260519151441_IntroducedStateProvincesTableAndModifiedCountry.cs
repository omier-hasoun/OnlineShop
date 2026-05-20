using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntroducedStateProvincesTableAndModifiedCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "Countries",
                type: "CHAR(3)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StateProvinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateProvinces", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_StateProvinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CurrencyCode",
                table: "Countries",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_StateProvinces_CountryId",
                table: "StateProvinces",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Currencies_CurrencyCode",
                table: "Countries",
                column: "CurrencyCode",
                principalTable: "Currencies",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Currencies_CurrencyCode",
                table: "Countries");

            migrationBuilder.DropTable(
                name: "StateProvinces");

            migrationBuilder.DropIndex(
                name: "IX_Countries_CurrencyCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "Countries");
        }
    }
}
