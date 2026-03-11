using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmLista.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cim = table.Column<string>(type: "TEXT", nullable: false),
                    Rendezo = table.Column<string>(type: "TEXT", nullable: false),
                    Mufaj = table.Column<string>(type: "TEXT", nullable: false),
                    MegjelenesiEv = table.Column<int>(type: "INTEGER", nullable: false),
                    IMDBPontszam = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmek", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filmek");
        }
    }
}
