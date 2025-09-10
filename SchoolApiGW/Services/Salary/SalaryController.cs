using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Middleware;

namespace SchoolApiGW.Services.Salary
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTMiddleware]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryClient _salaryClient;
        public SalaryController(ISalaryClient salaryClient)
        {
            _salaryClient = salaryClient;
        }


        [HttpPut("salary")]
        public async Task<IActionResult> SalaryActions([FromQuery] int actionType, [FromBody] SalaryData request)
        {
            #region Initialize Response
            ResponseModel response = new ResponseModel
            {
                IsSuccess = false,
                Status = -1,
                Message = "Invalid ActionType"
            };
            #endregion

            #region Get ClientId from JWT Claims
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            #endregion

            try
            {
                #region Switch ActionType
                switch (actionType)
                {
                    case 0:
                        #region Salary Release on Departments
                        if (request.Salary.Count > 0)
                            response = await _salaryClient.SalaryReleaseOnDepartments(request.Salary[0], clientId);
                        break;
                    #endregion

                    case 1:
                        #region Salary Release on Employee Code
                        if (request.Salary.Count > 0)
                            response = await _salaryClient.SalaryReleaseOnEmployeeCode(request.Salary[0], clientId);
                        break;
                    #endregion

                    case 2:
                        #region Update Salary Details
                        if (request.EMD.Count > 0)
                            response = await _salaryClient.UpdateSalaryDetails(request.EMD[0], clientId);
                        break;
                    #endregion

                    case 3:
                        #region Get Employee Salary To Edit
                        response = await _salaryClient.GetEmployeeSalaryToEdit(request.ECode, clientId);
                        break;
                    #endregion

                    case 4:
                        #region Update Salary Details On Field
                        response = await _salaryClient.UpdateSalaryDetailsOnField(request.EMD, clientId);
                        break;
                    #endregion

                    case 5:
                        #region Delete Salary On Employee Code
                        var salaryJson = JsonConvert.SerializeObject(request.Sal);
                        //  response = await _salaryService.AddNewLoan(salaryJson, clientId);
                        response = await _salaryClient.DeleteSalaryOnEmployeeCode(salaryJson, clientId);
                        break;
                    #endregion

                    case 6:
                        #region Delete Salary On Departments
                        response = await _salaryClient.DeleteSalaryOnDepartments(request.Salary, clientId);
                        break;
                    #endregion

                    case 7:
                        #region Get Demo Salary Statement
                        if (request.Salary.Count > 0)
                            response = await _salaryClient.GetDemoSalaryOnDepartments(request.Salary[0], clientId);
                        break;
                    #endregion

                    case 8:
                        #region Add New Loan
                        var salary = JsonConvert.SerializeObject(request.Sal);
                        response = await _salaryClient.AddNewLoan(salary, clientId);
                        //     response = await _salaryService.AddNewLoan(request.Sal, clientId);
                        break;
                    #endregion

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
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryController", "SalaryActions", ex.ToString());
                #endregion
            }

            #region Return Response
            return Ok(response);
            #endregion
        }




        [HttpGet("fetch-salary-data")]
        public async Task<ActionResult<ResponseModel>> FetchSalaryData([FromQuery] int actionType, [FromQuery] string? param)
        {
            #region Get ClientId from JWT Claims
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            #endregion

            #region Initialize Response
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Invalid Type ID",
                ResponseData = null
            };
            #endregion

            try
            {
                switch (actionType)
                {
                    case 0:
                        response = await _salaryClient.GetEmployeeSalaryToEditOnEDID(param, clientId);
                        break;

                    case 1:
                        response = await _salaryClient.GetEmployeeSalaryToEditOnECode(param, clientId);
                        break;

                    case 2:
                        response = await _salaryClient.GetEmployeeSalaryToEditOnFieldName(param, clientId);
                        break;

                    case 3:
                        response = await _salaryClient.GetSalaryDataOnMonthFromSalaryOnDeparts(param, clientId);
                        break;

                    case 4:
                        response = await _salaryClient.GetCalculatedGrossNetEtc(param, clientId);
                        break;

                    case 5:
                        response = await _salaryClient.GetCalculatedGrossNetEtcOnEDID(param, clientId);
                        break;

                    case 6:
                        response = await _salaryClient.GetSalaryDataOnYearFromSalaryOnECode(param, clientId);
                        break;

                    case 7:
                        response = await _salaryClient.GetLoanDefaultList(clientId);
                        break;

                    case 8:
                        response = await _salaryClient.SalaryPaymentAccountStatementOnEcodeAndDates(param, clientId);
                        break;

                    case 9:
                        response = await _salaryClient.GetAvailableNetSalaryOnMonthFromSalaryAndSalaryPaymentOnDeparts(param, clientId);
                        break;

                    case 10:
                        response = await _salaryClient.GetBankSalarySlipOnMonthFromSalaryAndSalaryPaymentOnDeparts(param, clientId);
                        break;

                    default:
                        response.Message = "Invalid Type ID!";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryController", "FetchSalaryData", ex.ToString());
            }

            return Ok(response);
        }
    }
}
