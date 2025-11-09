#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardAuthor_Authors_AuthorId",
                table: "AwardAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AwardAuthor_Awards_AwardId",
                table: "AwardAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AwardAuthor",
                table: "AwardAuthor");

            migrationBuilder.RenameTable(
                name: "AwardAuthor",
                newName: "AuthorAwardBridge");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Authors",
                newName: "Birthday");

            migrationBuilder.RenameIndex(
                name: "IX_AwardAuthor_AuthorId",
                table: "AuthorAwardBridge",
                newName: "IX_AuthorAwardBridge_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorAwardBridge",
                table: "AuthorAwardBridge",
                columns: new[] { "AwardId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardBridge_Authors_AuthorId",
                table: "AuthorAwardBridge",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAwardBridge_Awards_AwardId",
                table: "AuthorAwardBridge",
                column: "AwardId",
                principalTable: "Awards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Authors_AuthorId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAwardBridge_Awards_AwardId",
                table: "AuthorAwardBridge");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorAwardBridge",
                table: "AuthorAwardBridge");

            migrationBuilder.RenameTable(
                name: "AuthorAwardBridge",
                newName: "AwardAuthor");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Authors",
                newName: "DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorAwardBridge_AuthorId",
                table: "AwardAuthor",
                newName: "IX_AwardAuthor_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AwardAuthor",
                table: "AwardAuthor",
                columns: new[] { "AwardId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AwardAuthor_Authors_AuthorId",
                table: "AwardAuthor",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AwardAuthor_Awards_AwardId",
                table: "AwardAuthor",
                column: "AwardId",
                principalTable: "Awards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
