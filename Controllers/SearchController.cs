using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly BookContext _context;

        public SearchController(BookContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet("{searchTerm}")]
        public async Task<ActionResult<string>> GetBookItems(string searchTerm)
        {
            return await _context.GetSearchResults(searchTerm);
        }
    }
}
