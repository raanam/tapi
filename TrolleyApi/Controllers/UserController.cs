using Microsoft.AspNetCore.Mvc;
using TrolleyApi.User;

namespace TrolleyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get() =>  Ok(_userService.Get());
    }
}
