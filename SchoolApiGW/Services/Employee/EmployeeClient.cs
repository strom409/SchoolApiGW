using Newtonsoft.Json;
using SchoolApiGW.Helper;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;

namespace SchoolApiGW.Services.Employee
{
    public class EmployeeClient : ProxyBaseUrl, IEmployeeClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeeClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddNewEmployee(EmployeeDetail emp, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_AddNewEmployee;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    emp
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "AddNewEmployee", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding a new employee.", ex);
            }
        }

        public async Task<ResponseModel> UpdateEmployee(EmployeeDetail value, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_UpdateEmployee;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    value
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "UpdateEmployee", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating employee.", ex);
            }
        }

        public async Task<ResponseModel> UpdateMultipleEmployee(EmployeeDetail value, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_UpdateMultipleEmployee;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    value
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "UpdateMultipleEmployee", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating multiple employees.", ex);
            }
        }

        public async Task<ResponseModel> UpdateEmployeeMonthlyAttendance(EmployeeDetail value, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_UpdateEmployeeMonthlyAttendance;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    value
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "UpdateEmployeeMonthlyAttendance", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating employee monthly attendance.", ex);
            }
        }

        public async Task<ResponseModel> WithdrawEmployee(EmployeeDetail value, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_WithdrawEmployee;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    value
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "WithdrawEmployee", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while withdrawing employee.", ex);
            }
        }

        public async Task<ResponseModel> RejoinEmployee(EmployeeDetail value, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_RejoinEmployee;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    value
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "RejoinEmployee", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while rejoining employee.", ex);
            }
        }

        public async Task<ResponseModel> UpdateEmployeeDetailField(EmployeeDetail value, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_UpdateEmployeeDetailField;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    value
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "UpdateEmployeeDetailField", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating employee detail field.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeeByCode(string empCode, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeeByCode, empCode);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employee = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employee;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeeByCode", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employee by code: {empCode}.", ex);
            }
        }

        public async Task<ResponseModel> GetAllEmployeesByYear(string year, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetAllEmployeesByYear, year);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetAllEmployeesByYear", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by year: {year}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesBySubDept(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesBySubDept, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesBySubDept", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by sub department: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesByDesignation(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesByDesignation, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesByDesignation", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by designation: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesByStatus(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesByStatus, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesByStatus", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by status: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesByName(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesByName, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesByName", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by name: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesByField(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesByField, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesByField", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by field: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesByMobile(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesByMobile, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesByMobile", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by mobile: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesByParentage(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesByParentage, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesByParentage", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by parentage: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesByAddress(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesByAddress, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesByAddress", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees by address: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeesForAttendanceUpdate(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeesForAttendanceUpdate, param);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response.ResponseData.ToString());
                    response.ResponseData = employees;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeesForAttendanceUpdate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching employees for attendance update: {param}.", ex);
            }
        }

        public async Task<ResponseModel> GetEmployeeTableFields(string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_GetEmployeeTableFields;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeeTableFields", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching employee table fields.", ex);
            }
        }

        public async Task<ResponseModel> GetNextEmployeeCode(string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Employee_GetNextEmployeeCode; 

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetNextEmployeeCodeAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching next employee code.", ex);
            }
        }

    }
}
