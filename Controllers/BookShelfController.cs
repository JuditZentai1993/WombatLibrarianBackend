using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookShelfController : ControllerBase
    {
        private readonly BookShelfContext _context;

        public BookShelfController(BookShelfContext context)
        {
            _context = context;
        }

        // GET: api/BookShelf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookShelfItems()
        {
            return await _context.BookShelfItems
                .Include(bookShelfItem => bookShelfItem.Authors)
                .ToListAsync();
        }

        // GET: api/BookShelf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            var book = await _context.BookShelfItems
                .Include(bookShelfItem => bookShelfItem.Authors)
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

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            Console.WriteLine(book.ToString());
            _context.Authors.AddRange(book.Authors);
            _context.BookShelfItems.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id }, 
                book);
        }

        // DELETE: api/BookShelf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.BookShelfItems.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.BookShelfItems.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(string id)
        {
            return _context.BookShelfItems.Any(e => e.Id == id);
        }
    }
}
