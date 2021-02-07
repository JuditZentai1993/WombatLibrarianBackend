using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            _repository = repository;
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
            var wishlist = await _repository.GetWishlistItemByIdAsync(id);

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
        public async Task<IActionResult> RemoveBookFromWishlistById(int id)
        {
            var wishlist = await _repository.GetWishlistItemByIdAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            await _repository.RemoveBookFromWishlistByIdAsync(wishlist);

            return NoContent();
        }
    }
}
