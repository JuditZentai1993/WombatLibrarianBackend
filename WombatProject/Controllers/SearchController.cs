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
    public class SearchController : ControllerBase
    {
        private readonly IBookAPIService _apiService;
        private readonly IMapper _mapper;

        public SearchController(IBookAPIService service, IMapper mapper)
        {
            _apiService = service;
            _mapper = mapper;
        }

        // GET: api/Search/<searchphrase>
        [HttpGet("{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookItems(string searchTerm)
        {
            var result = await _apiService.GetSearchResultsAsync(searchTerm);
            var mappedResult = _mapper.Map<IEnumerable<Book>>(result);
            return Ok(mappedResult);
        }
    }
}
