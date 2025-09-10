using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Departments
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {

        private readonly IDepartmentsClient _departmentClient;

        public DepartmentsController(IDepartmentsClient departmentClient)
        {
            _departmentClient = departmentClient;
        }

        [HttpPost("department-add")]
        public async Task<ActionResult<ResponseModel>> DepartmentAdd([FromQuery] int actionType, [FromBody] SubDepartment value)
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
                    case 0: // Add Department
                        response = await _departmentClient.AddDepartment(value, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid action type for Department.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentController", "DepartmentAction", ex.ToString());
            }

            return Ok(response);
        }
        [HttpPut("update-department")]
        public async Task<ActionResult<ResponseModel>> UpdateDepartment([FromQuery] int actionType,
    [FromBody] SubDepartment value)
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
                    case 0: // Update Department
                        response = await _departmentClient.UpdateDepartment(value, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid action type for update operation.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentController", "UpdateDepartment", ex.ToString());
            }

            return Ok(response);
        }

        [HttpGet("get-department-data")]
        public async Task<ActionResult<ResponseModel>> GetDepartmentData([FromQuery] int actionType,
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
                Message = "Invalid action type!",
                ResponseData = null
            };

            try
            {
                switch (actionType)
                {

                    case 0:
                        // Get Department by ID
                        response = await _departmentClient.getDepartments(clientId);
                        break;
                    case 1:
                        // Get Department by ID
                        response = await _departmentClient.GetDepartmentById(id, clientId);
                        break;

                    // Future action types can be handled here
                    default:
                        response.IsSuccess = false;
                        response.Message = "Unsupported action type!";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentController", "GetDepartmentData", ex.ToString());
            }

            return Ok(response);
        }

        [HttpDelete("delete-department")]
        public async Task<ActionResult<ResponseModel>> DeleteDepartment([FromQuery] int actionType,
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
                    case 0: // Delete by ID
                        response = await _departmentClient.DeleteDepartment(id, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid action type for delete operation.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentController", "DeleteDepartment", ex.ToString());
            }

            return Ok(response);
        }

    }
}
