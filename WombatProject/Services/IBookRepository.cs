using System.Collections.Generic;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<object>> GetBooksFromBookshelfAsync();
        Task<Bookshelf> GetBookshelfItemByIdAsync(int id);
        Task<Bookshelf> AddBookToBookshelfAsync(Book book);
        Task<int> RemoveBookFromBookshelfByIdAsync(Bookshelf bookshelf);
        Task<IEnumerable<object>> GetBooksFromWishlistAsync();
        Task<Wishlist> GetWishlistItemByIdAsync(int id);
        Task<Wishlist> AddBookToWishlistAsync(Book book);
        Task<int> RemoveBookFromWishlistByIdAsync(Wishlist wishlist);
    }
}
