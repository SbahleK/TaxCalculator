using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaxCalculator.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initialise_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgressiveTaxRates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rate = table.Column<string>(type: "TEXT", nullable: false),
                    From = table.Column<decimal>(type: "TEXT", nullable: false),
                    To = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressiveTaxRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculationTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProgressiveTaxRates",
                columns: new[] { "Id", "From", "Rate", "To" },
                values: new object[,]
                {
                    { 1L, 0m, "10%", 8350m },
                    { 2L, 8351m, "15%", 33950m },
                    { 3L, 33951m, "25%", 82250m },
                    { 4L, 82251m, "28%", 171550m },
                    { 5L, 171551m, "33%", 372950m },
                    { 6L, 372951m, "35%", 79228162514264337593543950335m }
                });

            migrationBuilder.InsertData(
                table: "TaxCalculationTypes",
                columns: new[] { "Id", "PostalCode", "Type" },
                values: new object[,]
                {
                    { 1L, "7441", "Progressive" },
                    { 2L, "A100", "Flat Value" },
                    { 3L, "7000", "Flat Rate" },
                    { 4L, "1000", "Progressive" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressiveTaxRates");

            migrationBuilder.DropTable(
                name: "TaxCalculationTypes");
        }
    }
}
