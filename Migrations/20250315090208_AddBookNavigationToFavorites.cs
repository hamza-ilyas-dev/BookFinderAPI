using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBookNavigationToFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Favorites_BookId",
                table: "Favorites",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Books_BookId",
                table: "Favorites",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Books_BookId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_BookId",
                table: "Favorites");
        }
    }
}
