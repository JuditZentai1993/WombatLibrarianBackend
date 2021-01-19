using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private readonly IBookAPIService _apiService;

        public BookshelvesController(IBookAPIService service)
        {
            _apiService = service;
        }

        // GET: api/Bookshelves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookshelf>>> GetBookshelves()
        {
            return await _apiService.Context.Bookshelves.ToListAsync();
        }

        // GET: api/Bookshelves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookshelf>> GetBookshelf(int id)
        {
            var bookshelf = await _apiService.Context.Bookshelves.FindAsync(id);

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
            if (id != bookshelf.Id)
            {
                return BadRequest();
            }

            _apiService.Context.Entry(bookshelf).State = EntityState.Modified;

            try
            {
                await _apiService.Context.SaveChangesAsync();
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
            //_apiService.Context.Authors.AddRange(book.Authors);
            //_apiService.Context.Categories.AddRange(book.Categories);
            //_apiService.Context.Books.Add(book);
            //Bookshelf bookshelf = new Bookshelf() { BookId = book.Id };
            //_apiService.Context.Bookshelves.Add(bookshelf);
            //await _apiService.Context.SaveChangesAsync();
            var bookshelf = await _apiService.AddBookToBookshelf(book);

            return CreatedAtAction("GetBookshelf", new { id = bookshelf.Id }, bookshelf);
        }

        // DELETE: api/Bookshelves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookshelf(int id)
        {
            var bookshelf = await _apiService.Context.Bookshelves.FindAsync(id);
            if (bookshelf == null)
            {
                return NotFound();
            }

            _apiService.Context.Bookshelves.Remove(bookshelf);
            await _apiService.Context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookshelfExists(int id)
        {
            return _apiService.Context.Bookshelves.Any(e => e.Id == id);
        }
    }
}
