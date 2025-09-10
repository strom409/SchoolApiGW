using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Subjects
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSubjectsController : ControllerBase
    {
        private readonly IEmployeeSubjectsClient _subjectsClient;
        public EmployeeSubjectsController(IEmployeeSubjectsClient subjectsClient)
        {
            _subjectsClient=subjectsClient;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int actionType, [FromQuery] string? param)
        {
            #region Initialize Response
            var response = new ResponseModel
            {
                IsSuccess = false,
                Status = -1,
                Message = "Invalid ActionType"
            };
            #endregion

            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");


            try
            {
                #region Switch ActionType
                switch (actionType)
                {
                    case 0: // Get by ID
                        response = await _subjectsClient.GetEmployeeSubjectById(param, clientId);
                        break;

                    case 1: // Get all
                        response = await _subjectsClient.GetEmployeeSubjects(clientId);
                        break;

                    default:
                        #region Unknown ActionType
                        response.Message = "Unknown ActionType!";
                        break;
                        #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                #region Exception Handling
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeSubjectsController", "Get", ex.ToString());
                #endregion
            }

            #region Return Response
            return Ok(response);
            #endregion
        }
    }
}
