using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Middleware;
using System.Linq;

namespace SchoolApiGW.Services.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTMiddleware] // or use [Authorize] if you switch to built-in auth
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] RequestUserDto user)
        {
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            var result = await _userService.AddUser(user, clientId);

            return Ok(result);
        }
    }
}
