using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accountancy.Migrations
{
    public partial class InitilMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Household Model",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Household Model", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Accounting Information",
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
                    table.PrimaryKey("PK_Accounting Information", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounting Information_Household Model_HouseholdModelID",
                        column: x => x.HouseholdModelID,
                        principalTable: "Household Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounting Information_HouseholdModelID",
                table: "Accounting Information",
                column: "HouseholdModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounting Information");

            migrationBuilder.DropTable(
                name: "Household Model");
        }
    }
}
