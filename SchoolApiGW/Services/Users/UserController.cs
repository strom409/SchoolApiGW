using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;
using SchoolApiGW.Middleware;
using System.Linq;

namespace SchoolApiGW.Services.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTMiddleware] 
    public class UserController : ControllerBase
    {
        private readonly IUserClient _userClient;

        public UserController(IUserClient userClient)
        {
            _userClient = userClient;
        }

        [HttpPost("User")]
        public async Task<ActionResult<ResponseModel>> AddUser([FromQuery] int actionType, [FromForm] RequestUserDto request)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level !"
            };

            try
            {

                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");
                switch (actionType)
                {
                    case 0: // Add
                        response = await _userClient.AddUserAsync(request, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Status = -1;
                        response.Message = "Invalid actionType provided.";
                        break;
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.ToString();
                Helper.Error.ErrorBLL.CreateErrorLog("UserController", "AddUser",
                    ex.ToString()
                );

                return Ok(response);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<ResponseModel>> UpdateUser([FromQuery] int actionType, [FromForm] RequestUserDto request)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level !"
            };

            try
            {

                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");
                switch (actionType)
                {
                    case 0: // Add
                        response = await _userClient.UpdateUserAsync(request, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Status = -1;
                        response.Message = "Invalid actionType provided.";
                        break;
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.ToString();
                Helper.Error.ErrorBLL.CreateErrorLog("UserController", "UpdateUser",
                    ex.ToString()
                );

                return Ok(response);
            }
        }


        [HttpDelete("Users/{userId}")]
        public async Task<ActionResult<ResponseModel>> DeleteUser([FromQuery] int actionType, [FromRoute] int userId)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level !"
            };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");

                switch (actionType)
                {
                    case 0: // Delete
                        response = await _userClient.DeleteUserAsync(userId, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Status = -1;
                        response.Message = "Invalid actionType provided.";
                        break;
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.ToString();
                Helper.Error.ErrorBLL.CreateErrorLog("UserController", "DeleteUser",
                    ex.ToString()
                );

                return Ok(response);
            }
        }
    }
}
