using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;
using SchoolApiGW.Middleware;

namespace SchoolApiGW.Services.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTMiddleware]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeClient _employeeClient;
        public EmployeeController(IEmployeeClient employeeClient)
        {
            _employeeClient = employeeClient;
        }


        [HttpPost("manage")]
        public async Task<ActionResult<ResponseModel>> ManageEmployee(
    [FromQuery] int actionType,
    [FromBody] EmployeeDetail emp)
        {
            #region Initialize Response
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Invalid Request!"
            };
            #endregion

            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            #endregion

            try
            {
                switch (actionType)
                {
                    case 0: // Add New Employee
                        response = await _employeeClient.AddNewEmployee(emp, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Status = -1;
                        response.Message = "Invalid actionType!";
                        break;
                }
            }
            catch (Exception ex)
            {
                #region Handle Exception
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeController", "ManageEmployee", ex.ToString());
                #endregion
            }

            return Ok(response);
        }


        [HttpPut("manage")]
        public async Task<ActionResult<ResponseModel>> ManageEmployeeUpdates(
    [FromQuery] int actionType,
    [FromBody] EmployeeDetail value)
        {
            #region Initialize Response
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Invalid Request!"
            };
            #endregion

            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            #endregion

            try
            {
                #region Switch Case for Operations
                switch (actionType)
                {
                    case 0: // Update Employee Details
                        response = await _employeeClient.UpdateEmployee(value, clientId);
                        break;

                    case 1: // Update Employees Table On Field Name
                        response = await _employeeClient.UpdateMultipleEmployee(value, clientId);
                        break;

                    case 2: // Update Employees Monthly Attendance
                        response = await _employeeClient.UpdateEmployeeMonthlyAttendance(value, clientId);
                        break;

                    case 3: // Withdraw EMPLOYEE
                        response = await _employeeClient.WithdrawEmployee(value, clientId);
                        break;

                    case 4: // Rejoin EMPLOYEE
                        response = await _employeeClient.RejoinEmployee(value, clientId);
                        break;

                    case 5: // Update EmployeeDetails Table On Field Name
                        response = await _employeeClient.UpdateEmployeeDetailField(value, clientId);
                        break;

                    default:
                        response.Status = -1;
                        response.Message = "Invalid actionType!";
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {
                #region Handle Exception
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeController", "ManageEmployeeUpdates", ex.ToString());
                #endregion
            }

            return Ok(response);
        }


        [HttpGet("fetch")]
        public async Task<ActionResult<ResponseModel>> FetchEmployeeData([FromQuery] int actionType, [FromQuery] string? param)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "No Data Found!" };
            #region Get ClientId
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            #endregion

            try
            {
                switch (actionType)
                {
                    case 0: // Get Employee on Employee Code
                        if (string.IsNullOrEmpty(param))
                        {
                            response.IsSuccess = false;
                            response.Message = "Employee Code is required.";
                            return Ok(response);
                        }
                        response = await _employeeClient.GetEmployeeByCode(param, clientId);
                        break;

                    case 1: // On Year All
                        response = await _employeeClient.GetAllEmployeesByYear(param, clientId);
                        break;

                    case 2: // On SubDepartmentID, Year
                        response = await _employeeClient.GetEmployeesBySubDept(param, clientId);
                        break;

                    case 3: // On Designation, Year
                        response = await _employeeClient.GetEmployeesByDesignation(param, clientId);
                        break;

                    case 4: // On Status, Year
                        response = await _employeeClient.GetEmployeesByStatus(param, clientId);
                        break;

                    case 5: // By Name list
                        response = await _employeeClient.GetEmployeesByName(param, clientId);
                        break;

                    case 6: // By Table Fields
                        response = await _employeeClient.GetEmployeesByField(param, clientId);
                        break;

                    case 7: // By Mobile Number
                        response = await _employeeClient.GetEmployeesByMobile(param, clientId);
                        break;

                    case 8: // By Parentage
                        response = await _employeeClient.GetEmployeesByParentage(param, clientId);
                        break;

                    case 9: // By Address
                        response = await _employeeClient.GetEmployeesByAddress(param, clientId);
                        break;

                    case 10: // Employee Attendance Data
                        response = await _employeeClient.GetEmployeesForAttendanceUpdate(param, clientId);
                        break;

                    case 11: // Get Employee Table Fields
                        response = await _employeeClient.GetEmployeeTableFields(clientId);
                        break;
                    case 12: // Get Next Employee Code
                        response = await _employeeClient.GetNextEmployeeCode(clientId);
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
                response.Status = -1;
                response.Message = "Error occurred while processing request.";
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeController", "FetchEmployeeData", ex.ToString());
            }

            return Ok(response);
        }





    }
}
