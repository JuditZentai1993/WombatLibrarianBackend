using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WombatLibrarianApi.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<Book>>> GetAuthorBookItems(string author)
        {
            await Task.Run(() => _context.GetAuthorBooks(author));
            return await _context.AuthorBookItems.ToListAsync();
        }
    }
}
