using System.Collections.Generic;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public interface IBookAPIService
    {
        List<Book> SearchResults { get; }
        List<Book> AuthorBookItems { get; }
        Task GetSearchResultsAsync(string searchTerm);
        Task GetAuthorBooksAsync(string author);
    }
}
