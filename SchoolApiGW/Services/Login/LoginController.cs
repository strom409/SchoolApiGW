using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SchoolApiGW.Helper;
using Microsoft.AspNetCore.Authorization;

namespace SchoolApiGW.Services.login
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //  private readonly ILoginServices _loginService;
        private readonly ILoginClient _LoginClient;

        public LoginController(ILoginClient LoginClient)
        {
            _LoginClient = LoginClient;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel>> Login([FromBody] LoginDto login)
        {
            try
            {
                var result = await _LoginClient.LoginUser(login);
                if (result == null || !result.IsSuccess)
                    return Unauthorized(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel 
                { 
                    IsSuccess = false, 
                    Message = "An error occurred while processing your request.",
                    Status = 0,
                    Error = ex.Message
                });
            }
        }
    }
}
