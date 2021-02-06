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
        private readonly IBookAPIService _apiService;

        public AuthorController(IBookAPIService service)
        {
            _apiService = service;
        }

        // GET: api/Author/<authorname>
        [HttpGet("{author}")]
        public async Task<ActionResult<IEnumerable<BookItem>>> GetAuthorBookItems(string author)
        {
            return Ok(await _apiService.GetAuthorBooksAsync(author));
        }
    }
}
