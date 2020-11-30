using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace COMP2084_Assignment1.Data.Migrations
{
    public partial class CreateInitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Episodes = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    AirStart = table.Column<DateTime>(nullable: false),
                    AirEnd = table.Column<DateTime>(nullable: false),
                    Studios = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Animes_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Volumes = table.Column<int>(nullable: false),
                    Chapters = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    PublishStart = table.Column<DateTime>(nullable: false),
                    PublishEnd = table.Column<DateTime>(nullable: false),
                    Authors = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Mangas_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeID = table.Column<int>(nullable: false),
                    EpisodesWatched = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Photo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AnimeLists_AnimeID",
                        column: x => x.AnimeID,
                        principalTable: "Animes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaID = table.Column<int>(nullable: false),
                    VolumesRead = table.Column<int>(nullable: false),
                    ChaptersRead = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderId",
                        column: x => x.MangaID,
                        principalTable: "Mangas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeLists_AnimeID",
                table: "AnimeLists",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_GenreID",
                table: "Animes",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_MangaLists_MangaID",
                table: "MangaLists",
                column: "MangaID");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_GenreID",
                table: "Mangas",
                column: "GenreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeLists");

            migrationBuilder.DropTable(
                name: "MangaLists");

            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "Mangas");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
