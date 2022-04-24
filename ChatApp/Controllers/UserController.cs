using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models.Authentication;
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

    [Route("authenticate/{username}")]
    [HttpGet]
    public async Task<IActionResult> Authenticate(string username)
    {
        if (false)
        {
            var auth = await _userService.Authenticate(username); // optional utilise mediator + CQRS + DDD 

            if (auth != null)
            {
                return Ok(auth);
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            return Ok(new Authentication
            {
                ExpirationDate = System.DateTime.Now.AddDays(1),
                Token = "Token"
            });
        }
    }
}
