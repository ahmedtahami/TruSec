using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruSec.DAL.Migrations
{
    public partial class seedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54ad5996-76dd-4ead-a68c-4b113ccfc21a", "63a10e82-397d-407d-b18c-e6c8898188af", "Company Admin", "Company Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6cb46cc-1787-4396-b78f-5ebc8e44329d", "4e547369-ee11-4cdf-9139-99ba8c13d11a", "Super Admin", "Super Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f052d789-f5bd-4e2e-99aa-13f4d615784a", "e55154c8-2b03-4280-b7d5-0fa847f3acd0", "Monitoring Staff", "Monitoring Staff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54ad5996-76dd-4ead-a68c-4b113ccfc21a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6cb46cc-1787-4396-b78f-5ebc8e44329d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f052d789-f5bd-4e2e-99aa-13f4d615784a");
        }
    }
}
