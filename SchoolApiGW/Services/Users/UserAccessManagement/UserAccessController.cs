using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Users.UserAccessManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccessController : ControllerBase
    {
         private readonly IUserAccessClient _userAccessClient;
        public UserAccessController(IUserAccessClient userAccessClient)
        {
            _userAccessClient=userAccessClient; 
        }

        [HttpPost("add-user-access")]
        public async Task<ActionResult<ResponseModel>> AddUserAccess([FromQuery] int actionType, [FromBody] UserAccessDto value)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level !"
            };
            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");
            #endregion

            try
            {
                switch (actionType)
                {
                    case 0: // Add
                        response = await _userAccessClient.AddToUserAccessAsync(value, clientId);
                        break;
                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("UserAccessController", "AddUserAccess", ex.ToString());
            }

            return Ok(response);
        }

        [HttpPut("update-user-access")]
        public async Task<ActionResult<ResponseModel>> UpdateUserAccess([FromQuery] int actionType, [FromBody] UserAccessDto value)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level !"
            };
            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");
            #endregion
            try
            {
                switch (actionType)
                {
                    case 1: // Update
                        response = await _userAccessClient.UpdateUserAccessAsync(value, clientId);
                        break;
                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                  Helper.Error.ErrorBLL.CreateErrorLog("UserAccessController", "UpdateUserAccess", ex.ToString());
            }

            return Ok(response);
        }

        [HttpGet("get-user-types")]
        public async Task<ActionResult<ResponseModel>> GetUserTypes([FromQuery] int actionType)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level !"
            };

            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");
            #endregion

            try
            {
                switch (actionType)
                {
                    case 0: // Get All
                        response = await _userAccessClient.GetUserTypesAsync(clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType for fetching user types.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("UserTypeController", "GetUserTypes", ex.ToString());
            }

            return Ok(response);
        }

        [HttpDelete("delete-user-access")]
        public async Task<ActionResult<ResponseModel>> DeleteUserAccess([FromQuery] int actionType, [FromQuery] string uIDFK)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level !"
            };
            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");
            #endregion

            try
            {
                switch (actionType)
                {
                    case 0: // Delete
                        response = await _userAccessClient.DeleteUserAccessAsync(uIDFK, clientId);
                        break;
                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType for delete.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                 Helper.Error.ErrorBLL.CreateErrorLog("UserAccessController", "DeleteUserAccess", ex.ToString());
            }

            return Ok(response);
        }
    }
}
