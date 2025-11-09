#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "624ec277-c61d-4a4f-a0de-5642de11f625");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8d26a1b-4b33-403c-881f-af52fdbc673a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4475c3cf-c8be-49e6-baea-5a9d6dcf0017", null, "Librarian", "LIBRARIAN" },
                    { "ac7b16e7-d815-4586-b136-a83fc7a2c812", null, "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4475c3cf-c8be-49e6-baea-5a9d6dcf0017");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac7b16e7-d815-4586-b136-a83fc7a2c812");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "624ec277-c61d-4a4f-a0de-5642de11f625", null, "Librarian", "LIBRARIAN" },
                    { "c8d26a1b-4b33-403c-881f-af52fdbc673a", null, "Editor", "EDITOR" }
                });
        }
    }
}
