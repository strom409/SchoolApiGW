using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.ClassTest.Subject
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectClient _subjectClient;
        public SubjectController(ISubjectClient subjectClient)
        {
            _subjectClient=subjectClient;   
        }


        [HttpPost("subject-info")]
        public async Task<ActionResult<ResponseModel>> AddSubject([FromQuery] int actionType, [FromBody] SubjectDTO value)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0 };
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";

            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            try
            {
                switch (actionType)
                {
                    case 0: // Add Subject
                        return Ok(await _subjectClient.InsertNewSubject(value, clientId));

                    case 1: // Add Optional Subject
                        return Ok(await _subjectClient.InsertNewOptionalSubject(value, clientId));

                    case 2: // Add SubSubject
                        return Ok(await _subjectClient.InsertNewSubSubject(value, clientId));

                    default:
                        response.Message = "Invalid actionType";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectController", "AddOrUpdateSubject", ex.ToString());
                return StatusCode(500, response);
            }
        }

        [HttpPut("subject-info")]
        public async Task<ActionResult<ResponseModel>> UpdateSubject([FromQuery] int actionType, [FromBody] SubjectDTO value)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0 };
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";

            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            try
            {
                switch (actionType)
                {
                    case 0: // Update Subject
                        return Ok(await _subjectClient.UpdateSubject(value, clientId));

                    case 1: // Update Optional Subject
                        return Ok(await _subjectClient.UpdateOptionalSubject(value, clientId));

                    case 2: // Update SubSubject
                        return Ok(await _subjectClient.UpdateSubSubject(value, clientId));

                    default:
                        response.Message = "Invalid actionType";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectController", "UpdateSubject", ex.ToString());
                return StatusCode(500, response);
            }
        }



        [HttpGet("subject-info")]
        public async Task<ActionResult<ResponseModel>> FetchSubjectInfo(int actionType, string? param)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0 };
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";

            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            try
            {
                switch (actionType)
                {
                    case 0:
                        return Ok(await _subjectClient.GetSubjectsByClassId(param, clientId));

                    case 1:
                        return Ok(await _subjectClient.GetOptionalSubjectsByClassId(param, clientId));

                    case 2:
                        return Ok(await _subjectClient.GetSubSubjectsBySubjectId(param, clientId));

                    default:
                        response.Message = "Invalid actionType";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectController", "FetchSubjectInfo", ex.ToString());
                return StatusCode(500, response);
            }
        }


        [HttpDelete("subject-info")]
        public async Task<ActionResult<ResponseModel>> DeleteSubject(
     [FromQuery] int actionType,
     [FromQuery] string? param)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0 };
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";

            if (string.IsNullOrEmpty(param))
                return BadRequest("param (SubjectId) is required");

            try
            {
                switch (actionType)
                {
                    case 0: // Delete Subject
                        return Ok(await _subjectClient.DeleteSubject(param, clientId));

                    case 1: // Delete Optional Subject
                        return Ok(await _subjectClient.DeleteOptionalSubject(param, clientId));

                    case 2: // Delete SubSubject
                        return Ok(await _subjectClient.DeleteSubSubject(param, clientId));

                    default:
                        response.Message = "Invalid actionType";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectController", "DeleteSubject", ex.ToString());
                return StatusCode(500, response);
            }
        }
    }
}
