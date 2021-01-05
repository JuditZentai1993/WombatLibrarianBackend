using System.Collections.Generic;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public interface BookAPIService
    {
        WombatBooksContext Context { get; }
        List<Book> AuthorBookItems { get; set; }
        List<Book> BookItems { get; set; }
        Task GetSearchResults(string searchTerm);
        Task GetAuthorBooks(string author);
    }
}
