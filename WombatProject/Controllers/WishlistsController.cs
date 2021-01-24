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
    public class WishlistsController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public WishlistsController(IBookRepository repository)
        {
            this._repository = repository;
        }

        // GET: api/Wishlists
        [HttpGet]
        public async Task<IActionResult> GetWishlistItems()
        {
            var books = await _repository.GetBooksFromWishlistAsync();
            return Ok(books);
        }

        // GET: api/Wishlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wishlist>> GetWishlistItemById(int id)
        {
            var wishlist = await _repository.Context.Wishlists.FindAsync(id);

            if (wishlist == null)
            {
                return NotFound();
            }

            return wishlist;
        }

        // POST: api/Wishlists
        [HttpPost]
        public async Task<ActionResult<Wishlist>> AddItemToWishlist(Book book)
        {
            var wishlist = await _repository.AddBookToWishlistAsync(book);

            return CreatedAtAction("GetWishlistItemById", new { id = wishlist.Id }, wishlist);
        }

        // DELETE: api/Wishlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBookFromWishlist(int id)
        {
            var wishlist = await _repository.Context.Wishlists.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            _repository.Context.Wishlists.Remove(wishlist);
            await _repository.Context.SaveChangesAsync();

            return NoContent();
        }

        private bool WishlistExists(int id)
        {
            return _repository.Context.Wishlists.Any(e => e.Id == id);
        }
    }
}
