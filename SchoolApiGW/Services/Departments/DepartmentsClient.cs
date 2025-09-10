using SchoolApiGW.Helper;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SchoolApiGW.Services.Departments
{
    public class DepartmentsClient : ProxyBaseUrl, IDepartmentsClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public DepartmentsClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddDepartment(SubDepartment department, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Department_AddDepartment;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    department
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentsClient", "AddDepartment", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding department.", ex);
            }
        }

        public async Task<ResponseModel> UpdateDepartment(SubDepartment department, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Department_UpdateDepartment;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    department
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentsClient", "UpdateDepartment", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating department.", ex);
            }
        }

        public async Task<ResponseModel> DeleteDepartment(long id, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Department_DeleteDepartment, id);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentsClient", "DeleteDepartment", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting department.", ex);
            }
        }

        public async Task<ResponseModel> GetDepartmentById(long id, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Department_GetDepartmentById, id);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var dto = JsonConvert.DeserializeObject<SubDepartment>(response.ResponseData.ToString());
                    response.ResponseData = dto;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentsClient", "GetDepartmentById", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching department by ID: {id}.", ex);
            }
        }

        public async Task<ResponseModel> getDepartments(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Department_GetDepartments;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<SubDepartment>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DepartmentsClient", "getDepartments", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching departments.", ex);
            }
        }
    }
}
