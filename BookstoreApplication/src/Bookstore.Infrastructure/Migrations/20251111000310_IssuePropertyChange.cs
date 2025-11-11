using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IssuePropertyChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0160121d-ce9f-4356-b5da-225a2b87b641");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "befb5eca-ac67-4a53-bd76-394a8a84c757");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "ComicIssues",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "IssueNumber",
                table: "ComicIssues",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ComicIssues",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "86260be3-a452-4329-9169-67be69606f51", null, "Librarian", "LIBRARIAN" },
                    { "9bafaa68-c01a-4673-af40-010caac1c0a6", null, "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86260be3-a452-4329-9169-67be69606f51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bafaa68-c01a-4673-af40-010caac1c0a6");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ComicIssues",
                newName: "PictureUrl");

            migrationBuilder.AlterColumn<int>(
                name: "IssueNumber",
                table: "ComicIssues",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ComicIssues",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0160121d-ce9f-4356-b5da-225a2b87b641", null, "Editor", "EDITOR" },
                    { "befb5eca-ac67-4a53-bd76-394a8a84c757", null, "Librarian", "LIBRARIAN" }
                });
        }
    }
}
