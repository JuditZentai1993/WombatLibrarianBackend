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
            return await _context.Bookshelves.Include(bookshelf => bookshelf.Books)
                .Select( bookshelf => bookshelf.Books)
                .Select(book => book
                .Select(
                    book => new
                    {
                        Id = book.Id,
                        BookshelfId = book.Bookshelves.FirstOrDefault().Id,
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
                            Publisher = book.Publisher,
                            Authors = book.Authors,
                            Categories = book.Categories
                        }
                    })
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
                await _context.SaveChangesAsync();
                bookItem = book;
            }

            //var bookshelf = new Bookshelf();
            if (!_context.Bookshelves.Any())
            {
                var bookshelf = new Bookshelf() { Books = new List<Book>() };
                bookshelf.Books.Add(bookItem);
                _context.Bookshelves.Add(bookshelf);
            }
            else
            {
                var bookshelf = _context.Bookshelves.Include(bookshelf => bookshelf.Books).ToList();
                bookshelf.FirstOrDefault()
                    .Books.Add(bookItem);
            }

            //var bookshelf = new Bookshelf() { BookId = book.Id, book = book };
            //_context.Bookshelves.Add(bookshelf);
            await _context.SaveChangesAsync();
            return _context.Bookshelves.FirstOrDefault();
        }

        public async Task RemoveBookFromBookshelfByIdAsync(string bookid)
        {
            _context.Bookshelves
                .Include(bookshelf => bookshelf.Books)
                .FirstOrDefault()
                .Books.RemoveAll(book => book.Id == bookid);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetBooksFromWishlistAsync()
        {
            return await _context.Wishlists.Include(wishlist => wishlist.Books)
                .Select(wishlist => wishlist.Books)
                .Select(book => book
                .Select(
                    book => new
                    {
                        Id = book.Id,
                        WishlistId = book.Wishlists.FirstOrDefault().Id,
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
                            Publisher = book.Publisher,
                            Authors = book.Authors,
                            Categories = book.Categories
                        }
                    })
                )
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
                await _context.SaveChangesAsync();
                bookItem = book;
            }
            //var wishlist = new Wishlist()
            if (!_context.Wishlists.Any())
            {
                var wishlist = new Wishlist() { Books = new List<Book>() };
                wishlist.Books.Add(bookItem);
                _context.Wishlists.Add(wishlist);
            }
            else
            {
                var wishlist = _context.Wishlists.Include(wishlist => wishlist.Books).ToList();
                wishlist.FirstOrDefault()
                    .Books.Add(bookItem);
            }
            //var wishlist = new Wishlist() { BookId = book.Id, book = book };
            //_context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
            return _context.Wishlists.FirstOrDefault();
        }

        public async Task RemoveBookFromWishlistByIdAsync(string bookid)
        {
            _context.Wishlists
                .Include(wishlist => wishlist.Books)
                .FirstOrDefault()
                .Books.RemoveAll(book => book.Id == bookid);
            await _context.SaveChangesAsync();
        }
    }
}
