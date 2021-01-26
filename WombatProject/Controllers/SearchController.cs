using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WombatLibrarianApi.Models;
using WombatLibrarianApi.Services;

namespace WombatLibrarianApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IBookAPIService _apiService;

        public SearchController(IBookAPIService service)
        {
            _apiService = service;
        }

        // GET: api/Search/<searchphrase>
        [HttpGet("{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookItems(string searchTerm)
        {
            await _apiService.GetSearchResultsAsync(searchTerm);
            return _apiService.SearchResults;
        }
    }
}
