using Microsoft.EntityFrameworkCore.Migrations;

namespace WombatLibrarianApi.Migrations
{
    public partial class RefactorManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BookBookshelf",
                columns: table => new
                {
                    BooksId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookshelvesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookshelf", x => new { x.BooksId, x.BookshelvesId });
                    table.ForeignKey(
                        name: "FK_BookBookshelf_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBookshelf_Bookshelves_BookshelvesId",
                        column: x => x.BookshelvesId,
                        principalTable: "Bookshelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookWishlist",
                columns: table => new
                {
                    BooksId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WishlistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookWishlist", x => new { x.BooksId, x.WishlistsId });
                    table.ForeignKey(
                        name: "FK_BookWishlist_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookWishlist_Wishlists_WishlistsId",
                        column: x => x.WishlistsId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBookshelf_BookshelvesId",
                table: "BookBookshelf",
                column: "BookshelvesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookWishlist_WishlistsId",
                table: "BookWishlist",
                column: "WishlistsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBookshelf");

            migrationBuilder.DropTable(
                name: "BookWishlist");

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
    }
}
