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
    public class BookShelfController : ControllerBase
    {
        private readonly BookAPIService _apiService;

        public BookShelfController(BookAPIService service)
        {
            _apiService = service;
        }

        // GET: api/BookShelf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookShelfItems()
        {
            return await _apiService.Context.BookShelfItems
                .Include(bookShelfItem => bookShelfItem.Authors)
                .Include(bookShelfItem => bookShelfItem.Categories)
                .ToListAsync();
        }

        // GET: api/BookShelf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            var book = await _apiService.Context.BookShelfItems
                .Include(bookShelfItem => bookShelfItem.Authors)
                .Include(bookShelfItem => bookShelfItem.Categories)
                .FirstOrDefaultAsync(bookShelfItem => bookShelfItem.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/BookShelf/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(string id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _apiService.Context.Entry(book).State = EntityState.Modified;

            try
            {
                await _apiService.Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/BookShelf
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _apiService.Context.Authors.AddRange(book.Authors);
            _apiService.Context.Categories.AddRange(book.Categories);
            _apiService.Context.BookShelfItems.Add(book);
            await _apiService.Context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id }, 
                book);
        }

        // DELETE: api/BookShelf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _apiService.Context.BookShelfItems.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _apiService.Context.BookShelfItems.Remove(book);
            await _apiService.Context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(string id)
        {
            return _apiService.Context.BookShelfItems.Any(e => e.Id == id);
        }
    }
}
