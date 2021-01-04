using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WombatLibrarianApi.Models;
using WombatLibrarianApi.Services;

namespace WombatLibrarianApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly GoogleBooksAPIService _apiService;

        public AuthorController(GoogleBooksAPIService service)
        {
            _apiService = service;
        }

        // GET: api/Author/<authorname>
        [HttpGet("{author}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAuthorBookItems(string author)
        {
            await _apiService.GetAuthorBooks(author);
            return _apiService.AuthorBookItems;
        }
    }
}
