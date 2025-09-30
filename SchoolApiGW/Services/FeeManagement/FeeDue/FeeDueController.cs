﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.FeeManagement.FeeDue
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeDueController : ControllerBase
    {
        private readonly IFeeDueClient _feeDueClient;
        public FeeDueController(IFeeDueClient feeDueClient)
        {
            _feeDueClient=feeDueClient; 
        }
        [HttpPost("add")]
        public async Task<ActionResult<ResponseModel>> Add([FromQuery] int actionType, [FromBody] object request)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
                // var clientId = Request.Headers["X-Client-Id"].FirstOrDefault();
                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");

                switch (actionType)
                {
                    case 0: // Add single FeeDue
                        var feeDto = JsonConvert.DeserializeObject<FeeDueDTO>(request.ToString());
                        response = await _feeDueClient.AddFeeDue(feeDto, clientId);
                        break;



                    // You can add more cases here for other add types
                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueController", "Add", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error adding FeeDue.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }
        [HttpGet("fetch")]
        public async Task<ActionResult<ResponseModel>> Fetch([FromQuery] int actionType, [FromQuery] string? param)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
                //  var clientId = Request.Headers["X-Client-Id"].FirstOrDefault();
                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");

                switch (actionType)
                {
                    case 0: // Get FeeDue by Student Name
                        if (string.IsNullOrEmpty(param))
                        {
                            response.IsSuccess = false;
                            response.Message = "StudentName parameter missing.";
                            return BadRequest(response);
                        }
                        response = await _feeDueClient.GetFeeDueByStudentName(param, clientId);
                        break;

                    case 1: // Get FeeDue by Admission No
                       
                        response = await _feeDueClient.GetFeeDueByAdmissionNo(param, clientId);
                        break;

                    case 2: // Get FeeDue by ClassID
                        if (!long.TryParse(param, out long classId))
                        {
                            response.IsSuccess = false;
                            response.Message = "Invalid ClassID.";
                            return BadRequest(response);
                        }
                        response = await _feeDueClient.GetFeeDueByClassId(classId, clientId);
                        break;
                    case 3: // Get All Months
                        response = await _feeDueClient.GetAllMonths(clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueController", "Fetch", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error fetching FeeDue.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ResponseModel>> Delete([FromQuery] int actionType, [FromQuery] long feeDueID)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");

                switch (actionType)
                {
                    case 0: // Delete FeeDue by FeeDueID
                        if (feeDueID <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Invalid FeeDueID.";
                            return BadRequest(response);
                        }
                        response = await _feeDueClient.DeleteFeeDue(feeDueID, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueController", "Delete", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error deleting FeeDue.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }


        [HttpPut("update")]
        public async Task<ActionResult<ResponseModel>> Update([FromQuery] int actionType, [FromBody] object request)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = "client2";
                // var clientId = Request.Headers["X-Client-Id"].FirstOrDefault();
                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");


                switch (actionType)
                {
                    case 0: // Update single FeeDue
                        var feeDto = JsonConvert.DeserializeObject<FeeDueDTO>(request.ToString());
                        response = await _feeDueClient.UpdateFeeDue(feeDto, clientId);
                        break;

                    // You can add more cases for different update types here
                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueController", "Update", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error updating FeeDue.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }
    }
}
