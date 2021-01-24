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
    public class BookshelvesController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BookshelvesController(IBookRepository repository)
        {
            this._repository = repository;
        }

        // GET: api/Bookshelves
        [HttpGet]
        public async Task<IActionResult> GetBookshelfItems()
        {
            var books = await _repository.GetBooksFromBookshelf();
            return Ok(books);
        }

        // GET: api/Bookshelves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookshelf>> GetBookshelfItemById(int id)
        {
            var bookshelf = await _repository.GetBookshelfItemByIdAsync(id);

            if (bookshelf == null)
            {
                return NotFound();
            }

            return bookshelf;
        }

        // POST: api/Bookshelves
        [HttpPost]
        public async Task<ActionResult<Bookshelf>> AddItemToBookshelf(Book book)
        {
            var bookshelf = await _repository.AddBookToBookshelf(book);

            return CreatedAtAction("GetBookshelf", new { id = bookshelf.Id }, bookshelf);
        }

        // DELETE: api/Bookshelves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBookFromBookshelf(int id)
        {
            var bookshelf = await _repository.Context.Bookshelves.FindAsync(id);
            if (bookshelf == null)
            {
                return NotFound();
            }

            _repository.Context.Bookshelves.Remove(bookshelf);
            await _repository.Context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookshelfExists(int id)
        {
            return _repository.Context.Bookshelves.Any(e => e.Id == id);
        }
    }
}
