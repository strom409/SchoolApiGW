﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.FeeManagement.FeeStructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeStructureController : ControllerBase
    {
        private readonly IFeeStructureClient _feeStructureClient;
        public FeeStructureController(IFeeStructureClient feeStructureClient)
        {
            _feeStructureClient=feeStructureClient;

        }

        [HttpPost("add-info")]
        public async Task<ActionResult<ResponseModel>> AddInfo([FromQuery] int actionType, [FromBody] object request)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");

                switch (actionType)
                {
                    case 0: // Add FeeStructure
                        var feeDto = JsonConvert.DeserializeObject<FeeStructureDto>(request.ToString());
                        response = await _feeStructureClient.AddFeeStructure(feeDto, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureController", "AddInfo", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "An unexpected error occurred.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet("fetch")]
        public async Task<ActionResult<ResponseModel>> Fetch([FromQuery] int actionType, [FromQuery] string? param)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            try
            {
                switch (actionType)
                {
                    case 0: // Get All FeeStructures
                        response = await _feeStructureClient.GetAllFeeStructures(clientId);
                        break;

                    case 1: // Get FeeStructure By ID
                        if (!long.TryParse(param, out long fsId))
                            return BadRequest(response);

                        response = await _feeStructureClient.GetFeeStructureById(fsId, clientId);
                        break;
                    case 2: // Get FeeStructures By Class (CIDFK)
                        if (!long.TryParse(param, out long cIdFk))
                        {
                            response.IsSuccess = false;
                            response.Status = 0;
                            response.Message = "Invalid Class ID (CIDFK).";
                            return BadRequest(response);
                        }

                        response = await _feeStructureClient.GetFeeStructuresByClassId(cIdFk, clientId);
                        break;
                    default:
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureController", "Fetch", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Error = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPut("update-by-action")]
        public async Task<ActionResult<ResponseModel>> Update([FromQuery] int actionType, [FromBody] object dto)
        {
            var response = new ResponseModel { IsSuccess = false, Status = 0, Message = "Invalid request." };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing");

                switch (actionType)
                {
                    case 0: // Update FeeStructure
                        var feeDto = JsonConvert.DeserializeObject<FeeStructureDto>(dto.ToString());
                        if (feeDto.FSID <= 0)
                            return BadRequest(new ResponseModel { IsSuccess = false, Status = 0, Message = "Invalid FSID for update." });

                        response = await _feeStructureClient.UpdateFeeStructure(feeDto, clientId);
                        break;

                    default:
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureController", "UpdateByAction", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "An error occurred during update.";
                response.Error = ex.Message;
            }

            return StatusCode(200, response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int actionType, [FromQuery] long id)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
                if (string.IsNullOrEmpty(clientId))
                    return BadRequest("ClientId header missing.");

                switch (actionType)
                {
                    case 0: // Delete FeeStructure
                        response = await _feeStructureClient.DeleteFeeStructure(id, "System", clientId); // pass UpdatedBy from header/body if needed
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Status = 0;
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error occurred during deletion.";
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureController", "DeleteByAction", ex.ToString());
            }

            return StatusCode(response.Status == 0 ? 400 : 200, response);
        }
    }
}
