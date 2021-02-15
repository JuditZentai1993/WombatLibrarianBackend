using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            _repository = repository;
        }

        // GET: api/Bookshelves
        [HttpGet]
        public async Task<IActionResult> GetBookshelfItems()
        {
            var books = await _repository.GetBooksFromBookshelfAsync();
            return Ok(books.FirstOrDefault());
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
            var bookshelf = await _repository.AddBookToBookshelfAsync(book);

            return Ok(bookshelf);
        }

        // DELETE: api/Bookshelves/5
        [HttpDelete("{bookshelfid}/{bookid}")]
        public async Task<IActionResult> RemoveBookFromBookshelfById(int bookshelfid, string bookid)
        {
            var bookshelf = await _repository.GetBookshelfItemByIdAsync(bookshelfid);
            if (bookshelf == null)
            {
                return NotFound();
            }

           await _repository.RemoveBookFromBookshelfByIdAsync(bookid);

            return NoContent();
        }
    }
}
