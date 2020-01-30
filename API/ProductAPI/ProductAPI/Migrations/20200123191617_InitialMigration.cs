using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(maxLength: 30, nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: false),
                    manufacturingDate = table.Column<DateTime>(nullable: false),
                    expiryDate = table.Column<DateTime>(nullable: false),
                    productType = table.Column<string>(maxLength: 10, nullable: false),
                    manufacturer = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
