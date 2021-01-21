using System.Collections.Generic;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public interface IBookAPIService
    {
        WombatBooksContext Context { get; }
        List<Book> AuthorBookItems { get; set; }
        List<Book> SearchResults { get; set; }
        Task GetSearchResults(string searchTerm);
        Task GetAuthorBooks(string author);
        Task<IEnumerable<object>> GetBooksFromBookshelf();
        Task<Bookshelf> AddBookToBookshelf(Book book);
        Task<IEnumerable<object>> GetBooksFromWishlist();
        Task<Wishlist> AddBookToWishlist(Book book);

    }
}
