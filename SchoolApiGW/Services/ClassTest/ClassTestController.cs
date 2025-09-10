using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.ClassTest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassTestController : ControllerBase
    {

        private readonly IClassTestClient _classTestClient;

        public ClassTestController(IClassTestClient classTestClient)
        {
            _classTestClient = classTestClient;
        }


        [HttpPost("class-test")]
        public async Task<ActionResult<ResponseModel>> AddClassTest(
     [FromQuery] int actionType,
     [FromBody] List<ClassTestDTO> value)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };
            // Extract clientId from header (or from JWT claims if applicable)
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            try
            {
                if (value == null || value.Count == 0)
                {
                    return BadRequest(new ResponseModel { IsSuccess = false, Error = "Invalid payload." });
                }

                switch (actionType)
                {
                    case 0: // Add Class Test Max Marks
                        return Ok(await _classTestClient.AddClassTestMaxMarks(value, clientId));

                    case 1: // Add Class Test Marks
                        return Ok(await _classTestClient.AddClassTestMarks(value, clientId));

                    default:
                        return BadRequest(new ResponseModel { IsSuccess = false, Error = "Invalid ActionType." });
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestController", "PostClassTest", ex.ToString());
                return StatusCode(500, response);
            }
        }

        [HttpPut("class-test/update")]
        public async Task<IActionResult> UpdateClassTest([FromQuery] int actionType, [FromBody] List<ClassTestDTO> value)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            // Extract clientId from header (or from JWT claims if applicable)
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            try
            {
                if (value == null || value.Count == 0)
                    return BadRequest(new ResponseModel { IsSuccess = false, Error = "Request body is empty or invalid." });

                switch (actionType)
                {
                    case 0: // Update class test max marks
                        response = await _classTestClient.UpdateClassTestMaxMarks(value, clientId);
                        break;

                    case 1: // Edit/update class test marks
                        response = await _classTestClient.EditUpdateClassTestMarks(value, clientId);
                        break;

                    default:
                        response.Message = "Invalid ActionType.";
                        return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestController", "UpdateClassTest", ex.ToString());
                return StatusCode(500, response);
            }
        }

        [HttpGet("class-test")]
        public async Task<ActionResult<ResponseModel>> FetchClassTest(int actionType, string? param)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0 };
            // Extract clientId from header (or from JWT claims if applicable)
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            try
            {
                switch (actionType)
                {
                    case 0:
                        return Ok(await _classTestClient.GetSubjectForMaxMarks(param, clientId));

                    case 1:
                        return Ok(await _classTestClient.GetMaxMarks(param, clientId));

                    case 2:
                        return Ok(await _classTestClient.GetStudents(param, clientId));

                    case 3:
                        return Ok(await _classTestClient.GetStudentsWithMarks(param, clientId));

                    case 4:
                        return Ok(await _classTestClient.ViewDateWiseResult(param, clientId));

                    case 5:
                        return Ok(await _classTestClient.ClassTestReport(param, clientId));

                    case 6:
                        return Ok(await _classTestClient.ViewDateWiseResultForAllSubjects(param, clientId));

                    case 7:
                        return Ok(await _classTestClient.ViewDateWiseResultForTotalMarks(param, clientId));

                    case 8:
                        return Ok(await _classTestClient.ViewDateWiseTotalMMandObtMarks(param, clientId));

                    case 9:
                        return Ok(await _classTestClient.GetMissingClassTestMarks(param, clientId));

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
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestController", "FetchClassTest", ex.ToString());
                return StatusCode(500, response);
            }
        }
    }
}
