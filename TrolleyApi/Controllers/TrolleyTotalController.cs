using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrolleyApi.TrolleyTotal;

namespace TrolleyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        private readonly ITrolleyTotalCalculatorService _service;
        private readonly ILogger<TrolleyTotalController> _logger;

        public TrolleyTotalController(ITrolleyTotalCalculatorService service,
            ILogger<TrolleyTotalController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CalculateTrolleyTotalRequest request)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));
            return Ok(_service.Calculate(request));
        }
    }
}
