using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxManager.Api.Migrations
{
    public partial class TaxManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MunicipalityId = table.Column<int>(nullable: false),
                    TaxType = table.Column<int>(nullable: false),
                    TaxValue = table.Column<decimal>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxEntries_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Vilnius" });

            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Kaunas" });

            migrationBuilder.InsertData(
                table: "TaxEntries",
                columns: new[] { "Id", "DateFrom", "DateTo", "MunicipalityId", "TaxType", "TaxValue" },
                values: new object[,]
                {
                    { 1, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 0.2m },
                    { 2, new DateTime(2016, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 0.4m },
                    { 3, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 0.1m },
                    { 4, new DateTime(2016, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 0.1m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxEntries_MunicipalityId",
                table: "TaxEntries",
                column: "MunicipalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxEntries");

            migrationBuilder.DropTable(
                name: "Municipalities");
        }
    }
}
