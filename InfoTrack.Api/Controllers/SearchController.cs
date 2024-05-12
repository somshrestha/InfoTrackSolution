using InfoTrack.Api.Dtos;
using InfoTrack.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        [Route("GetSearchResult")]
        public async Task<IActionResult> GetSearchResult([FromBody] SearchDto dto)
        {
            try
            {
                var result = await _searchService.Search(dto);

                return result == null ? NotFound() : Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllSearchHistory")]
        public async Task<IActionResult> GetAllSearchHistory()
        {
            try
            {
                var result = await _searchService.GetAllSearchHistory();

                return result.Count == 0 ? NotFound() : Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
