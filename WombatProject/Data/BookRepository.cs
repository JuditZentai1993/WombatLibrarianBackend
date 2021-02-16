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

        public async Task<IEnumerable<Book>> GetBooksFromBookshelfAsync()
        {
            var bookshelf = await _context.Bookshelves.Include(bookshelf => bookshelf.Books).ToListAsync();

            return bookshelf.FirstOrDefault().Books;
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

        public async Task<IEnumerable<Book>> GetBooksFromWishlistAsync()
        {
            var wishlist = await _context.Wishlists.Include(wishlist => wishlist.Books).ToListAsync();

            return wishlist.FirstOrDefault().Books;
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
