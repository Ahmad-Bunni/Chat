using System.Threading.Tasks;
using ChatApp.Domain.Interface;
using ChatApp.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
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

        [Route("authenticate/{username}")]
        [HttpGet]
        public async Task<IActionResult> Authenticate(string username)
        {
            Authentication auth = await _userService.Authenticate(username);

            if (auth != null)
            {
                return new JsonResult(auth)
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            else
            {
                return BadRequest();
            }
        }
    }
}