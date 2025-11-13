using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexForBookTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12f105c5-af2e-4b2d-a86a-790986368c33"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("26382cdc-00e2-4f59-b92f-fc4762a4b697"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("381d59f9-b364-4da2-bf72-bf2eacdee1f4"), null, "Editor", "EDITOR" },
                    { new Guid("b8c35fac-a3f9-4db3-b929-cd0f3b8070df"), null, "Librarian", "LIBRARIAN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Title",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("381d59f9-b364-4da2-bf72-bf2eacdee1f4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8c35fac-a3f9-4db3-b929-cd0f3b8070df"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("12f105c5-af2e-4b2d-a86a-790986368c33"), null, "Librarian", "LIBRARIAN" },
                    { new Guid("26382cdc-00e2-4f59-b92f-fc4762a4b697"), null, "Editor", "EDITOR" }
                });
        }
    }
}
