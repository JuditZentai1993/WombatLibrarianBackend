using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public class BookRepository : IBookRepository
    {
        protected WombatBooksContext _context { get; }

        public BookRepository(WombatBooksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetBooksFromBookshelfAsync()
        {
            var bookIds = _context.Bookshelves.Select(book => book.BookId).ToList();

            return await _context.Books
               .Where(book => bookIds.Contains(book.Id))
               .Include(bookShelfItem => bookShelfItem.Authors)
               .Include(bookShelfItem => bookShelfItem.Categories)
               .Join(_context.Bookshelves,
               book => book.Id,
               bookshelf => bookshelf.BookId
               ,
                (book, bookshelf) => new
                {
                    Id = book.Id,
                    BookshelfId = bookshelf.Id,
                    VolumeInfo = new {
                        Title = book.Title,
                        Subtitle = book.Subtitle,
                        ImageLinks  = new
                        {
                            Thumbnail = book.Thumbnail
                        },
                        Description = book.Description,
                        PageCount = book.PageCount,
                        AverageRating = book.Rating,
                        RatingsCount = book.RatingCount,
                        Language = book.Language,
                        MaturityRating = book.MaturityRating,
                        PublishedDate = book.Published,
                        Publisher = book.Publisher
                    }
                }
                )
                 .ToListAsync();
        }

        public async Task<Bookshelf> GetBookshelfItemByIdAsync(int id)
        {
            return await _context.Bookshelves.FindAsync(id);
        }

        public async Task<Bookshelf> AddBookToBookshelfAsync(Book book)
        {
            var bookItem = _context.Books.Where(item => item.Id == book.Id).FirstOrDefault();
            if (bookItem == null)
            {
                _context.Authors.AddRange(book.Authors);
                _context.Categories.AddRange(book.Categories);
                _context.Books.Add(book);
            }
            var bookshelf = new Bookshelf() { BookId = book.Id, book = book };
            _context.Bookshelves.Add(bookshelf);
            await _context.SaveChangesAsync();
            return bookshelf;
        }

        public async Task RemoveBookFromBookshelfByIdAsync(Bookshelf bookshelf)
        {
            _context.Bookshelves.Remove(bookshelf);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetBooksFromWishlistAsync()
        {
            var bookIds = _context.Wishlists.Select(book => book.BookId).ToList();
            return await _context.Books
                .Where(book => bookIds.Contains(book.Id))
                .Include(wishlistItem => wishlistItem.Authors)
                .Include(wishlistItem => wishlistItem.Categories)
                .Join(_context.Wishlists,
                book => book.Id,
                wishlist => wishlist.BookId,
                (book, wishlist) => new
                {
                    Id = book.Id,
                    WishlistId = wishlist.Id,
                    VolumeInfo = new
                    {
                        Title = book.Title,
                        Subtitle = book.Subtitle,
                        ImageLinks = new
                        {
                            Thumbnail = book.Thumbnail
                        },
                        Description = book.Description,
                        PageCount = book.PageCount,
                        AverageRating = book.Rating,
                        RatingsCount = book.RatingCount,
                        Language = book.Language,
                        MaturityRating = book.MaturityRating,
                        PublishedDate = book.Published,
                        Publisher = book.Publisher
                    }
                })
                .ToListAsync();
        }

        public async Task<Wishlist> GetWishlistItemByIdAsync(int id)
        {
            return await _context.Wishlists.FindAsync(id);
        }

        public async Task<Wishlist> AddBookToWishlistAsync(Book book)
        {
            var bookItem = _context.Books.Where(item => item.Id == book.Id).FirstOrDefault();

            if (bookItem == null)
            {
                _context.Authors.AddRange(book.Authors);
                _context.Categories.AddRange(book.Categories);
                _context.Books.Add(book);
            }
            var wishlist = new Wishlist() { BookId = book.Id, book = book };
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
            return wishlist;
        }

        public async Task RemoveBookFromWishlistByIdAsync(Wishlist wishlist)
        {
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
        }
    }
}
