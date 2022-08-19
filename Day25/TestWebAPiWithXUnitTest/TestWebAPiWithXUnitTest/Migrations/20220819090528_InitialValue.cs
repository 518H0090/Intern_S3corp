using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebAPiWithXUnitTest.Migrations
{
    public partial class InitialValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CharacterJob = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    CharacterLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_CharacterName",
                table: "Characters",
                column: "CharacterName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}