using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentFaster.Migrations
{
    /// <inheritdoc />
    public partial class Words : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Word_Games_GameID",
                table: "Word");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Word",
                table: "Word");

            migrationBuilder.RenameTable(
                name: "Word",
                newName: "Words");

            migrationBuilder.RenameIndex(
                name: "IX_Word_GameID",
                table: "Words",
                newName: "IX_Words_GameID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Words",
                table: "Words",
                column: "WordID");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Games_GameID",
                table: "Words",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Games_GameID",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "Word");

            migrationBuilder.RenameIndex(
                name: "IX_Words_GameID",
                table: "Word",
                newName: "IX_Word_GameID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Word",
                table: "Word",
                column: "WordID");

            migrationBuilder.AddForeignKey(
                name: "FK_Word_Games_GameID",
                table: "Word",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
