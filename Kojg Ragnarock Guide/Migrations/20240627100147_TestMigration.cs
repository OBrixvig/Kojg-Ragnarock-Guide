using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kojg_Ragnarock_Guide.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d4a9b9b-9392-4129-a732-bf81fc6bcda9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8275667-e23b-4768-9e0e-cfdfce9b8b9e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34f6922c-6370-4252-a809-ad6f72e795c6", null, "admin", "admin" },
                    { "e184e162-1535-40ae-8d52-2c278ab92037", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34f6922c-6370-4252-a809-ad6f72e795c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e184e162-1535-40ae-8d52-2c278ab92037");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6d4a9b9b-9392-4129-a732-bf81fc6bcda9", null, "client", "client" },
                    { "d8275667-e23b-4768-9e0e-cfdfce9b8b9e", null, "admin", "admin" }
                });
        }
    }
}
