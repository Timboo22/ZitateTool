using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanoaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMitarbeiterToZitate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BenutzerId",
                table: "Zitate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zitate_BenutzerId",
                table: "Zitate",
                column: "BenutzerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zitate_Benutzer_BenutzerId",
                table: "Zitate",
                column: "BenutzerId",
                principalTable: "Benutzer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zitate_Benutzer_BenutzerId",
                table: "Zitate");

            migrationBuilder.DropIndex(
                name: "IX_Zitate_BenutzerId",
                table: "Zitate");

            migrationBuilder.DropColumn(
                name: "BenutzerId",
                table: "Zitate");
        }
    }
}
