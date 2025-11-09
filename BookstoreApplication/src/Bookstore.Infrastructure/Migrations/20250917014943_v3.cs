#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Birthday", "FullName" },
                values: new object[,]
                {
                    { 1, "English novelist", new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Jane Austen" },
                    { 2, "American writer", new DateTime(1835, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Mark Twain" },
                    { 3, "English modernist author", new DateTime(1882, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Virginia Woolf" },
                    { 4, "American novelist", new DateTime(1899, 7, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Ernest Hemingway" },
                    { 5, "English crime writer", new DateTime(1890, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Agatha Christie" }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "Description", "Name", "YearBegan" },
                values: new object[,]
                {
                    { 1, "Award for best novel", "Best Novel", 1950 },
                    { 2, "Award for lifetime contribution", "Lifetime Achievement", 1960 },
                    { 3, "Award for first book", "Debut Author", 1970 },
                    { 4, "Voted by readers", "Readers Choice", 1980 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "80 Strand, London", "Penguin Books", "https://penguin.co.uk" },
                    { 2, "195 Broadway, New York", "HarperCollins", "https://harpercollins.com" },
                    { 3, "1745 Broadway, New York", "Random House", "https://randomhouse.com" }
                });

            migrationBuilder.InsertData(
                table: "AuthorAwardBridge",
                columns: new[] { "AuthorId", "AwardId", "Year" },
                values: new object[,]
                {
                    { 1, 1, 1951 },
                    { 2, 1, 1952 },
                    { 3, 1, 1953 },
                    { 5, 1, 1955 },
                    { 1, 2, 1965 },
                    { 4, 2, 1970 },
                    { 5, 2, 1975 },
                    { 2, 3, 1971 },
                    { 3, 3, 1972 },
                    { 5, 3, 1973 },
                    { 1, 4, 1981 },
                    { 2, 4, 1982 },
                    { 3, 4, 1983 },
                    { 4, 4, 1984 },
                    { 5, 4, 1985 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "1111111111111", 432, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Pride and Prejudice" },
                    { 2, 1, "1111111111112", 384, new DateTime(1815, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Emma" },
                    { 3, 2, "2222222222221", 366, new DateTime(1884, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Adventures of Huckleberry Finn" },
                    { 4, 2, "2222222222222", 274, new DateTime(1876, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "The Adventures of Tom Sawyer" },
                    { 5, 3, "3333333333331", 296, new DateTime(1925, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Mrs Dalloway" },
                    { 6, 3, "3333333333332", 320, new DateTime(1927, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 3, "To the Lighthouse" },
                    { 7, 4, "4444444444441", 127, new DateTime(1952, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "The Old Man and the Sea" },
                    { 8, 4, "4444444444442", 355, new DateTime(1929, 9, 27, 0, 0, 0, 0, DateTimeKind.Utc), 2, "A Farewell to Arms" },
                    { 9, 5, "5555555555551", 256, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Murder on the Orient Express" },
                    { 10, 5, "5555555555552", 272, new DateTime(1939, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), 3, "And Then There Were None" },
                    { 11, 1, "1111111111113", 248, new DateTime(1817, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Persuasion" },
                    { 12, 2, "6666666666661", 232, new DateTime(1903, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "The Call of the Wild" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumns: new[] { "AuthorId", "AwardId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
