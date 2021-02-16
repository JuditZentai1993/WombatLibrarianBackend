using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthorController(IBookAPIService service, IMapper mapper)
        {
            _apiService = service;
            _mapper = mapper;
        }

        // GET: api/Author/<authorname>
        [HttpGet("{author}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAuthorBookItems(string author)
        {
            var result = await _apiService.GetAuthorBooksAsync(author);
            var mappedResult = _mapper.Map<IEnumerable<Book>>(result);
            return Ok(mappedResult);
        }
    }
}
