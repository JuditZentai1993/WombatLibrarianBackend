using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorContext _context;

        public AuthorController(AuthorContext context)
        {
            _context = context;
        }

        // GET: api/Author
        [HttpGet("{author}")]
        public async Task<ActionResult<string>> GetAuthorBookItems(string author)
        {
            return await _context.GetAuthorBooks(author);
        }
    }
}
