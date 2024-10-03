using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassionProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class artistcards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ArtistId",
                table: "Cards",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Artists_ArtistId",
                table: "Cards",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Artists_ArtistId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_ArtistId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Cards");
        }
    }
}
