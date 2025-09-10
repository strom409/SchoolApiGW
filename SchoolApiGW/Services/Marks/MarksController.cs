using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Marks
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IMarksClient _marksClient;
        public MarksController(IMarksClient marksClient)
        {
            _marksClient= marksClient;
        }

        [HttpPost("save")]
        public async Task<ActionResult<ResponseModel>> Save([FromBody] MarksDto marksDto, [FromQuery] int actionType)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");

                switch (actionType)
                {
                    case 1: // Add Marks
                       
                            response = await _marksClient.AddMarks(marksDto, clientId);
                            // Optionally, you can accumulate responses if needed
                       
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MarksController", "Save", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Unexpected error occurred.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }


        [HttpPut("update")]
        public async Task<ActionResult<ResponseModel>> Update([FromBody] MarksDto dto, [FromQuery] int actionType)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level!"
            };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");

                switch (actionType)
                {
                    case 2: // Update Marks
                        response = await _marksClient.UpdateMarks(dto, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MarksController", "Update", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Unexpected error occurred.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet("fetch")]
        public async Task<ActionResult<ResponseModel>> Get([FromQuery] int actionType, [FromQuery] string? param = null)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");

                switch (actionType)
                {
                    case 1: // Get by ID (param = MarksID)
                        response = await _marksClient.GetMarksWithNames(param, clientId);
                        break;



                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MarksController", "Get", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Unexpected error occurred.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ResponseModel>> Delete([FromQuery] int actionType, [FromQuery] string marksId)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level!"
            };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");

                switch (actionType)
                {
                    case 1: // Delete action
                        if (string.IsNullOrEmpty(marksId))
                        {
                            response.IsSuccess = false;
                            response.Message = "marksId parameter is required for delete.";
                            return BadRequest(response);
                        }

                        response = await _marksClient.DeleteMarks(marksId, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType for delete.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MarksController", "Delete", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Unexpected error occurred.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }

    }
}

