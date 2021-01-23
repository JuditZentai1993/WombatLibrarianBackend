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
        public async Task<IActionResult> GetBookshelves()
        {
            var books = await _repository.GetBooksFromBookshelf();
            return Ok(books);
        }

        // GET: api/Bookshelves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookshelf>> GetBookshelf(int id)
        {
            var bookshelf = await _repository.GetBookShelveByIdAsync(id);

            if (bookshelf == null)
            {
                return NotFound();
            }

            return bookshelf;
        }

        // PUT: api/Bookshelves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookshelf(int id, Bookshelf bookshelf)
        {
            // validate input params -> id != bookshelf.Id -> return BadRequest();
            // try {
            // _repository.UpdateBookShelf(id, bookshelf);
            // } catch (ArgumentException ex){
            //  return NotFound();
            // }
            // return NoContent();
            if (id != bookshelf.Id)
            {
                return BadRequest();
            }

            _repository.Context.Entry(bookshelf).State = EntityState.Modified;

            try
            {
                await _repository.Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookshelfExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookshelves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookshelf>> PostBookshelf(Book book)
        {
            var bookshelf = await _repository.AddBookToBookshelf(book);

            return CreatedAtAction("GetBookshelf", new { id = bookshelf.Id }, bookshelf);
        }

        // DELETE: api/Bookshelves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookshelf(int id)
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
