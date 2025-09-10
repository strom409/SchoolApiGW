using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.EmpStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpStatusController : ControllerBase
    {
        private readonly IEmpStatusClient _empStatusClient;
        public EmpStatusController(IEmpStatusClient empStatusClient)
        {
            _empStatusClient = empStatusClient;
        }

        [HttpGet("get-employee-status")]
        public async Task<ActionResult<ResponseModel>> GetEmployeeStatus([FromQuery] int actionType)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level !" };
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            try
            {
                switch (actionType)
                {
                    case 0:
                        response = await _empStatusClient.GetEmployeeStatus(clientId);
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
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeStatusController", "GetEmployeeStatus", ex.ToString());
            }

            return Ok(response);
        }

    }
}

