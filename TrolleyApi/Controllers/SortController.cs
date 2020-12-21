using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TrolleyApi.Sort;

namespace TrolleyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly ISortService _sortService;

        public SortController(ISortService sortService)
        {
            _sortService = sortService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get(
            [FromQuery]
            [Required] 
            [SortOptionValidator]
        string sortOption)
            => Ok(await _sortService.Sort(sortOption.ToUpperInvariant()));
    }
}
