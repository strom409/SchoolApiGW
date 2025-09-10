﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.ExamGrades
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamGradesController : ControllerBase
    {
        private readonly IExamGradesClient _examGradesClient;
        public ExamGradesController(IExamGradesClient examGradesClient)
        {
            _examGradesClient=examGradesClient;
        }

        [HttpGet("fetch-examgrades")]
        public async Task<ActionResult<ResponseModel>> FetchExamGrades([FromQuery] int actionType, [FromQuery] string? param)
        {
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "No Data Found!" };

            try
            {
                switch (actionType)
                {
                    case 0:
                        response = await _examGradesClient.GetExamGrades(clientId);
                        break;

                    case 1:
                        if (long.TryParse(param, out long id))
                        {
                            response = await _examGradesClient.GetExamGradeById(id, clientId);
                        }
                        else
                        {
                            response = new ResponseModel { IsSuccess = false, Status = 0, Message = "Invalid ID parameter!" };
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
                response = new ResponseModel { IsSuccess = false, Status = -1, Message = "Error: " + ex.Message };
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesController", "FetchExamGrades", ex.ToString());
            }

            return Ok(response);
        }

        [HttpPost("add-examgrade")]
        public async Task<ActionResult<ResponseModel>> AddExamGrade([FromQuery] int actionType, [FromBody] ExamGradesDTO value)
        {
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "issue in controller!" };
            try
            {
                switch (actionType)
                {
                    case 0:
                        response = await _examGradesClient.AddExamGradeAsync(value, clientId);
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
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesController", "AddExamGrade", ex.ToString());
            }

            return Ok(response);
        }

        [HttpPut("update-examgrade")]
        public async Task<ActionResult<ResponseModel>> UpdateExamGrade([FromQuery] int actionType, [FromBody] ExamGradesDTO value)
        {
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            var response = new ResponseModel();
            try
            {
                switch (actionType)
                {
                    case 0:

                        response = await _examGradesClient.UpdateExamGradeAsync(value, clientId);
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
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesController", "UpdateExamGrade", ex.ToString());
            }

            return Ok(response);
        }

        [HttpDelete("delete-examgrade")]
        public async Task<ActionResult<ResponseModel>> DeleteExamGrade([FromQuery] int actionType, [FromQuery] long id)
        {
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            var response = new ResponseModel();
            try
            {
                switch (actionType)
                {
                    case 0:

                        response = await _examGradesClient.DeleteExamGradeAsync(id, clientId);
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
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesController", "DeleteExamGrade", ex.ToString());
            }

            return Ok(response);
        }
    }
}
