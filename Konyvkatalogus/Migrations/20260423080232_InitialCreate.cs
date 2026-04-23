using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Konyvkatalogus.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Konyv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Cim = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    KiadasIdeje = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Szerzo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Kategoria = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Isbn = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konyv", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Konyv",
                columns: new[] { "Id", "Cim", "Isbn", "Kategoria", "KiadasIdeje", "Szerzo" },
                values: new object[,]
                {
                    { 1, "A Gyűrűk Ura: A Gyűrű Szövetsége", "978-0-618-00222-8", "Fantasy", new DateTime(1954, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.R.R. Tolkien" },
                    { 2, "Harry Potter és a Bölcsek Köve", "978-0-7475-3269-9", "Fantasy", new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.K. Rowling" },
                    { 3, "A kis herceg", "978-0-15-601219-5", "Mese", new DateTime(1943, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Antoine de Saint-Exupéry" },
                    { 4, "1984", "978-0-452-28423-4", "Disztópia", new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "George Orwell" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konyv");
        }
    }
}
