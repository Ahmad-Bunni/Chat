using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Route("authenticate/username/{username}")]
    [HttpGet]
    public async Task<IActionResult> Authenticate(string username)
    {
        var auth = await _userService.Authenticate(username); // optional utilise mediator + CQRS + DDD 

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
