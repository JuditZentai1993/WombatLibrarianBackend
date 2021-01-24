using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public class BookRepository : IBookRepository
    {
        public WombatBooksContext Context { get; }

        public BookRepository(WombatBooksContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<object>> GetBooksFromBookshelfAsync()
        {
            var bookIds = Context.Bookshelves.Select(book => book.BookId).ToList();
            return await Context.Books
                .Where(book => bookIds.Contains(book.Id))
                .Include(bookShelfItem => bookShelfItem.Authors)
                .Include(bookShelfItem => bookShelfItem.Categories)
                .Join(Context.Bookshelves,
                book => book.Id,
                bookshelf => bookshelf.BookId,
                (book, bookshelf) => new
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Thumbnail = book.Thumbnail,
                    Description = book.Description,
                    PageCount = book.PageCount,
                    Rating = book.Rating,
                    RatingCount = book.RatingCount,
                    Language = book.Language,
                    MaturityRating = book.MaturityRating,
                    Published = book.Published,
                    Publisher = book.Publisher,
                    BookshelfId = bookshelf.Id
                })
                .ToListAsync();
        }

        public async Task<Bookshelf> GetBookshelfItemByIdAsync(int id)
        {
            return await Context.Bookshelves.FindAsync(id);
        }

        public async Task<Bookshelf> AddBookToBookshelfAsync(Book book)
        {
            var bookItem = Context.Books.Where(item => item.Id == book.Id).FirstOrDefault();
            if (bookItem == null)
            {
                Context.Authors.AddRange(book.Authors);
                Context.Categories.AddRange(book.Categories);
                Context.Books.Add(book);
            }
            Bookshelf bookshelf = new Bookshelf() { BookId = book.Id };
            Context.Bookshelves.Add(bookshelf);
            await Context.SaveChangesAsync();
            return bookshelf;

        }

        public async Task<int> RemoveBookFromBookshelfById(Bookshelf bookshelf)
        {
            Context.Bookshelves.Remove(bookshelf);
            return await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetBooksFromWishlist()
        {
            var bookIds = Context.Wishlists.Select(book => book.BookId).ToList();
            return await Context.Books
                .Where(book => bookIds.Contains(book.Id))
                .Include(wishlistItem => wishlistItem.Authors)
                .Include(wishlistItem => wishlistItem.Categories)
                .Join(Context.Wishlists,
                book => book.Id,
                wishlist => wishlist.BookId,
                (book, wishlist) => new
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Thumbnail = book.Thumbnail,
                    Description = book.Description,
                    PageCount = book.PageCount,
                    Rating = book.Rating,
                    RatingCount = book.RatingCount,
                    Language = book.Language,
                    MaturityRating = book.MaturityRating,
                    Published = book.Published,
                    Publisher = book.Publisher,
                    WishlistId = wishlist.Id
                })
                .ToListAsync();
        }

        public async Task<Wishlist> AddBookToWishlist(Book book)
        {
            var bookItem = Context.Books.Where(item => item.Id == book.Id).FirstOrDefault();

            if (bookItem == null)
            {
                Context.Authors.AddRange(book.Authors);
                Context.Categories.AddRange(book.Categories);
                Context.Books.Add(book);
            }
            Wishlist wishlist = new Wishlist() { BookId = book.Id };
            Context.Wishlists.Add(wishlist);
            await Context.SaveChangesAsync();
            return wishlist;
        }
    }
}
