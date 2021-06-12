using Microsoft.EntityFrameworkCore.Migrations;

namespace DocApp2.Migrations
{
    public partial class dfjsdlkfjlkdsf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HujatTuris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HujjatNomi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HujatTuris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Viloyats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViloyatNomi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viloyats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hujjats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TuliqIsmi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViloyatId = table.Column<int>(type: "int", nullable: true),
                    HujjatTuriId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hujjats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hujjats_HujatTuris_HujjatTuriId",
                        column: x => x.HujjatTuriId,
                        principalTable: "HujatTuris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hujjats_Viloyats_ViloyatId",
                        column: x => x.ViloyatId,
                        principalTable: "Viloyats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hujjats_HujjatTuriId",
                table: "Hujjats",
                column: "HujjatTuriId");

            migrationBuilder.CreateIndex(
                name: "IX_Hujjats_ViloyatId",
                table: "Hujjats",
                column: "ViloyatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hujjats");

            migrationBuilder.DropTable(
                name: "HujatTuris");

            migrationBuilder.DropTable(
                name: "Viloyats");
        }
    }
}
