using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassionProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class newview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AlbumArtists_AlbumArtistId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "AlbumArtistId",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AlbumArtists_AlbumArtistId",
                table: "Albums",
                column: "AlbumArtistId",
                principalTable: "AlbumArtists",
                principalColumn: "AlbumArtistId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AlbumArtists_AlbumArtistId",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "AlbumArtistId",
                table: "Albums",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AlbumArtists_AlbumArtistId",
                table: "Albums",
                column: "AlbumArtistId",
                principalTable: "AlbumArtists",
                principalColumn: "AlbumArtistId");
        }
    }
}
