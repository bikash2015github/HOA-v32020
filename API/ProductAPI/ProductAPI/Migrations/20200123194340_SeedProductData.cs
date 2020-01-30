using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAPI.Migrations
{
    public partial class SeedProductData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "description", "expiryDate", "manufacturer", "manufacturingDate", "name", "productType" },
                values: new object[,]
                {
                    { "0001", "Android SmartPhone", new DateTime(2019, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oneplus", new DateTime(2019, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oneplus", "mobile" },
                    { "0002", "HP Thinkpad", new DateTime(2030, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HP", new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hp Thinkpad", "Laptop" },
                    { "0003", "Nestle Cofee Classic 200g", new DateTime(2019, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nestle", new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nestle Cofee", "Cofee" },
                    { "0004", "Brother Printer L23251", new DateTime(2030, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother Printer", "Printer" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: "0001");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: "0002");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: "0003");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: "0004");
        }
    }
}
