using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.Employee;
using System.Net.Http;

namespace SchoolApiGW.Services.Salary
{
    public class SalaryClient : ProxyBaseUrl, ISalaryClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public SalaryClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> SalaryReleaseOnDepartments(Salary salary, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_SalaryReleaseOnDepartments;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    salary
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "SalaryReleaseOnDepartments", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while releasing salary on departments.", ex);
            }
        }

        public async Task<ResponseModel> SalaryReleaseOnEmployeeCode(Salary salary, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_SalaryReleaseOnEmployeeCode;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    salary
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "SalaryReleaseOnEmployeeCode", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while releasing salary on employee code.", ex);
            }
        }

        public async Task<ResponseModel> UpdateSalaryDetails(EmployeeDetail employeeDetail, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_UpdateSalaryDetails;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    employeeDetail
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "UpdateSalaryDetails", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating salary details.", ex);
            }
        }

        public async Task<ResponseModel> UpdateSalaryDetailsOnField(List<EmployeeDetail> employeeDetails, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_UpdateSalaryDetailsOnField;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    employeeDetails
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "UpdateSalaryDetailsOnField", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating salary details on field.", ex);
            }
        }

        public async Task<ResponseModel> DeleteSalaryOnEmployeeCode(string sal, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_DeleteSalaryOnEmployeeCode;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Delete,
                    sal
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "DeleteSalaryOnEmployeeCode", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting salary on employee code.", ex);
            }
        }

        public async Task<ResponseModel> DeleteSalaryOnDepartments(List<Salary> salary, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_DeleteSalaryOnDepartments;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Delete,
                    salary
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "DeleteSalaryOnDepartments", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting salary on departments.", ex);
            }
        }

        public async Task<ResponseModel> GetDemoSalaryOnDepartments(Salary salary, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_GetDemoSalaryOnDepartments;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    salary
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetDemoSalaryOnDepartments", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting demo salary on departments.", ex);
            }
        }

        public async Task<ResponseModel> AddNewLoan(string salary, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_AddNewLoan;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    salary
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "AddNewLoan", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding new loan.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeeSalaryToEdit(string eCode, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_GetEmployeeSalaryToEdit;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salary = JsonConvert.DeserializeObject<Salary>(response.ResponseData.ToString());
                    response.ResponseData = salary;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetEmployeeSalaryToEdit", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting employee salary to edit: {eCode}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeeSalaryToEditOnEDID(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetEmployeeSalaryToEditOnEDID, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salary = JsonConvert.DeserializeObject<Salary>(response.ResponseData.ToString());
                    response.ResponseData = salary;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetEmployeeSalaryToEditOnEDID", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting employee salary to edit on EDID: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeeSalaryToEditOnECode(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetEmployeeSalaryToEditOnECode, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salary = JsonConvert.DeserializeObject<Salary>(response.ResponseData.ToString());
                    response.ResponseData = salary;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetEmployeeSalaryToEditOnECode", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting employee salary to edit on ECode: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeeSalaryToEditOnFieldName(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetEmployeeSalaryToEditOnFieldName, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salary = JsonConvert.DeserializeObject<Salary>(response.ResponseData.ToString());
                    response.ResponseData = salary;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetEmployeeSalaryToEditOnFieldName", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting employee salary to edit on field name: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetSalaryDataOnMonthFromSalaryOnDeparts(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetSalaryDataOnMonthFromSalaryOnDeparts, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salaryList = JsonConvert.DeserializeObject<List<Salary>>(response.ResponseData.ToString());
                    response.ResponseData = salaryList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetSalaryDataOnMonthFromSalaryOnDeparts", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting salary data on month from salary on departments: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetCalculatedGrossNetEtc(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetCalculatedGrossNetEtc, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salary = JsonConvert.DeserializeObject<Salary>(response.ResponseData.ToString());
                    response.ResponseData = salary;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetCalculatedGrossNetEtc", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting calculated gross net etc: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetCalculatedGrossNetEtcOnEDID(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetCalculatedGrossNetEtcOnEDID, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salary = JsonConvert.DeserializeObject<Salary>(response.ResponseData.ToString());
                    response.ResponseData = salary;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetCalculatedGrossNetEtcOnEDID", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting calculated gross net etc on EDID: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetSalaryDataOnYearFromSalaryOnECode(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetSalaryDataOnYearFromSalaryOnECode, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salaryList = JsonConvert.DeserializeObject<List<Salary>>(response.ResponseData.ToString());
                    response.ResponseData = salaryList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetSalaryDataOnYearFromSalaryOnECode", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting salary data on year from salary on ECode: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetLoanDefaultList(string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Salary_GetLoanDefaultList;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var loanList = JsonConvert.DeserializeObject<List<Salary>>(response.ResponseData.ToString());
                    response.ResponseData = loanList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetLoanDefaultList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting loan default list.", ex);
            }
        }

        public async Task<ResponseModel> SalaryPaymentAccountStatementOnEcodeAndDates(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_SalaryPaymentAccountStatementOnEcodeAndDates, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var paymentList = JsonConvert.DeserializeObject<List<Salary>>(response.ResponseData.ToString());
                    response.ResponseData = paymentList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "SalaryPaymentAccountStatementOnEcodeAndDates", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting salary payment account statement on ECode and dates: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetAvailableNetSalaryOnMonthFromSalaryAndSalaryPaymentOnDeparts(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetAvailableNetSalaryOnMonthFromSalaryAndSalaryPaymentOnDeparts, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salaryList = JsonConvert.DeserializeObject<List<Salary>>(response.ResponseData.ToString());
                    response.ResponseData = salaryList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetAvailableNetSalaryOnMonthFromSalaryAndSalaryPaymentOnDeparts", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting available net salary on month from salary and salary payment on departments: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetBankSalarySlipOnMonthFromSalaryAndSalaryPaymentOnDeparts(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Salary_GetBankSalarySlipOnMonthFromSalaryAndSalaryPaymentOnDeparts, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var salaryList = JsonConvert.DeserializeObject<List<Salary>>(response.ResponseData.ToString());
                    response.ResponseData = salaryList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SalaryClient", "GetBankSalarySlipOnMonthFromSalaryAndSalaryPaymentOnDeparts", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while getting bank salary slip on month from salary and salary payment on departments: {param}.", ex);
            }
        }
    }
}
