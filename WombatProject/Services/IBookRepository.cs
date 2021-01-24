using System.Collections.Generic;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public interface IBookRepository
    {
        WombatBooksContext Context { get; }

        Task<IEnumerable<object>> GetBooksFromBookshelf();
        Task<Bookshelf> GetBookshelfItemByIdAsync(int id);
        Task<Bookshelf> AddBookToBookshelf(Book book);
        Task<IEnumerable<object>> GetBooksFromWishlist();
        Task<Wishlist> AddBookToWishlist(Book book);
    }
}
