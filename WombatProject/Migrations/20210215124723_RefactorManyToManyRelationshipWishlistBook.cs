using Microsoft.EntityFrameworkCore.Migrations;

namespace WombatLibrarianApi.Migrations
{
    public partial class RefactorManyToManyRelationshipWishlistBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Bookshelves");

            migrationBuilder.AddColumn<int>(
                name: "BookshelfId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WishlistId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookshelfId",
                table: "Books",
                column: "BookshelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_WishlistId",
                table: "Books",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Bookshelves_BookshelfId",
                table: "Books",
                column: "BookshelfId",
                principalTable: "Bookshelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Wishlists_WishlistId",
                table: "Books",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Bookshelves_BookshelfId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Wishlists_WishlistId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookshelfId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_WishlistId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookshelfId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "BookId",
                table: "Wishlists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookId",
                table: "Bookshelves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
