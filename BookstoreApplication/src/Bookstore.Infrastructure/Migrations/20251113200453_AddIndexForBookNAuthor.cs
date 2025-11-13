using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexForBookNAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cba69b6b-a75a-4c15-9e34-e5b4de1cf140"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1dd44af-eaf2-47d3-a744-b48cd26896ec"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("12f105c5-af2e-4b2d-a86a-790986368c33"), null, "Librarian", "LIBRARIAN" },
                    { new Guid("26382cdc-00e2-4f59-b92f-fc4762a4b697"), null, "Editor", "EDITOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublishedDate",
                table: "Books",
                column: "PublishedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_DateOfBirth",
                table: "Authors",
                column: "DateOfBirth");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_FullName",
                table: "Authors",
                column: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_PublishedDate",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_DateOfBirth",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_FullName",
                table: "Authors");

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
                    { new Guid("cba69b6b-a75a-4c15-9e34-e5b4de1cf140"), null, "Librarian", "LIBRARIAN" },
                    { new Guid("e1dd44af-eaf2-47d3-a744-b48cd26896ec"), null, "Editor", "EDITOR" }
                });
        }
    }
}
