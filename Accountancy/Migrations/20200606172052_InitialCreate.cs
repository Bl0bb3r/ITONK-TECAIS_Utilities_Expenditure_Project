using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accountancy.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseholdModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccountancyInformation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseholdModelID = table.Column<int>(nullable: false),
                    BillCategory = table.Column<string>(nullable: true),
                    NetVal = table.Column<double>(nullable: false),
                    TimestampDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountancyInformation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountancyInformation_HouseholdModel_HouseholdModelID",
                        column: x => x.HouseholdModelID,
                        principalTable: "HouseholdModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountancyInformation_HouseholdModelID",
                table: "AccountancyInformation",
                column: "HouseholdModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountancyInformation");

            migrationBuilder.DropTable(
                name: "HouseholdModel");
        }
    }
}
