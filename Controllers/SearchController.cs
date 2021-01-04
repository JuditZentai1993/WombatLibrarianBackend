using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WombatLibrarianApi.Models;
using WombatLibrarianApi.Services;

namespace WombatLibrarianApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly GoogleBooksAPIService _apiService;

        public SearchController(GoogleBooksAPIService service)
        {
            _apiService = service;
        }

        // GET: api/Books
        [HttpGet("{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookItems(string searchTerm)
        {
            await _apiService.GetSearchResults(searchTerm);
            return _apiService.BookItems;
        }
    }
}
