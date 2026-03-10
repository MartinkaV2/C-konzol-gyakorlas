using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diakok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nev = table.Column<string>(type: "TEXT", nullable: false),
                    NeptunKod = table.Column<string>(type: "TEXT", nullable: false),
                    Szak = table.Column<string>(type: "TEXT", nullable: false),
                    BeiratkozasEve = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diakok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kurzusok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KurzusNev = table.Column<string>(type: "TEXT", nullable: false),
                    Oktato = table.Column<string>(type: "TEXT", nullable: false),
                    Kredit = table.Column<int>(type: "INTEGER", nullable: false),
                    FelelosTanszek = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurzusok", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diakok");

            migrationBuilder.DropTable(
                name: "Kurzusok");
        }
    }
}
