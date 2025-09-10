using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Result
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentResultsController : ControllerBase
    {
        private readonly IStudentResultsClient _studentResultsClient;
        public StudentResultsController(IStudentResultsClient studentResultsClient)
        {
            _studentResultsClient=studentResultsClient;
        }


        [HttpPost("fetch")]
        public async Task<ActionResult<ResponseModel>> Fetch([FromQuery] int actionType, [FromBody] object request)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level!"
            };

            try
            {
                string clientId = Request.Headers["X-Client-Id"].FirstOrDefault() ?? "client1";

                switch (actionType)
                {
                    case 1: // Fetch main student results
                        {
                            var studentRequest = JsonConvert.DeserializeObject<StudentResultsRequestDto>(request.ToString()!);
                            response = await _studentResultsClient.GetStudentResultsAsync(studentRequest, clientId);

                            break;
                        }

                    case 2: // Fetch optional student results
                        {
                            var optionalRequest = JsonConvert.DeserializeObject<OptionalResultsRequestDto>(request.ToString()!);
                            response = await _studentResultsClient.GetOptionalResultsAsync(optionalRequest, clientId);
                            break;
                        }

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentResultsController", "Fetch", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Unexpected error occurred.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }
    }
}
