using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kojg_Ragnarock_Guide.Migrations
{
    /// <inheritdoc />
    public partial class AppUserDatabaseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "420b2b7c-8338-42b8-b985-e87e32f64edb", null, "admin", "admin" },
                    { "683b89d4-2a67-4585-a829-8d4b5b5ccf42", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "420b2b7c-8338-42b8-b985-e87e32f64edb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "683b89d4-2a67-4585-a829-8d4b5b5ccf42");
        }
    }
}
