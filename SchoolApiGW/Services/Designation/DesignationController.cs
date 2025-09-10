using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Designation
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationClient _designationClient;
        public DesignationController(IDesignationClient designationClient)
        {
            _designationClient = designationClient;
        }

        [HttpGet("fetch-designation-data")]
        public async Task<ActionResult<ResponseModel>> FetchDesignationData([FromQuery] int actionType, [FromQuery] string? param)
        {
            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            #endregion

            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "No Data Found!" };

            try
            {
                switch (actionType)
                {
                    case 0:
                        response = await _designationClient.GetDesignations(clientId);
                        break;

                    case 1:
                        if (long.TryParse(param, out long id))
                        {
                            response = await _designationClient.GetDesignationById(id, clientId);
                        }
                        else
                        {
                            response = new ResponseModel
                            {
                                IsSuccess = false,
                                Status = 0,
                                Message = "Invalid ID parameter!",
                                ResponseData = null
                            };
                        }
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Message = "Error: " + ex.Message,
                    ResponseData = null
                };

                Helper.Error.ErrorBLL.CreateErrorLog("DesignationController", "FetchDesignationData", ex.ToString());
            }

            return Ok(response);
        }

        [HttpPost("add-designation")]
        public async Task<ActionResult<ResponseModel>> AddDesignation([FromQuery] int actionType, [FromBody] DesignationsModel value)
        {
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            ResponseModel response = new ResponseModel();

            try
            {
                switch (actionType)
                {
                    case 0: // Add
                        response = await _designationClient.AddDesignationAsync(value, clientId);
                        break;
                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("DesignationController", "ManageDesignation", ex.ToString());
            }

            return Ok(response);
        }


        [HttpPut("update-designation")]
        public async Task<ActionResult<ResponseModel>> UpdateDesignation([FromQuery] int actionType, [FromBody] DesignationsModel value)
        {
            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            #endregion

            #region Initialize Response
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Invalid action",
                ResponseData = null
            };
            #endregion

            try
            {
                switch (actionType)
                {
                    case 1: // Update designation
                        response = await _designationClient.UpdateDesignationAsync(value, clientId);
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
                Helper.Error.ErrorBLL.CreateErrorLog("DesignationController", "ManageDesignation", ex.ToString());
            }

            return Ok(response);
        }

        [HttpDelete("delete-designation")]
        public async Task<ActionResult<ResponseModel>> DeleteDesignation(
      [FromQuery] int actionType,
      [FromQuery] long id)
        {
            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            #endregion

            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Invalid action type",
                ResponseData = null
            };

            try
            {
                switch (actionType)
                {
                    case 0: // Delete by ID (standard)
                        response = await _designationClient.DeleteDesignationAsync(id, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid action type for delete";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("DesignationController", "DeleteDesignation", ex.ToString());
            }

            return Ok(response);
        }

    }
}
