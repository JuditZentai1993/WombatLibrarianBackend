using System.Collections.Generic;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public interface IBookAPIService
    {
        Task<IList<BookItem>> GetSearchResultsAsync(string searchTerm);
        Task<IList<BookItem>> GetAuthorBooksAsync(string author);
        Task<IList<BookItem>> SerializeResultsFromGoogleBooksApiAsync(string url);
    }
}
