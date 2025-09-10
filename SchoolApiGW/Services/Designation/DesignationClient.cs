using Microsoft.Extensions.Configuration;
using SchoolApiGW.Helper;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic; // Added for List<DesignationDTO>
using Newtonsoft.Json; // Added for JsonConvert

namespace SchoolApiGW.Services.Designation
{
    public class DesignationClient : ProxyBaseUrl, IDesignationClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public DesignationClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> GetDesignations(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Designation_GetDesignations;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<DesignationsModel>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DesignationClient", "GetDesignations", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching designations.", ex);
            }
        }

        public async Task<ResponseModel> GetDesignationById(long id, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Designation_GetDesignationById, id);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var dto = JsonConvert.DeserializeObject<DesignationsModel>(response.ResponseData.ToString());
                    response.ResponseData = dto;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DesignationClient", "GetDesignationById", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching designation by ID: {id}.", ex);
            }
        }

        public async Task<ResponseModel> AddDesignationAsync(DesignationsModel designation, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Designation_AddDesignation;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    designation
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DesignationClient", "AddDesignationAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding designation.", ex);
            }
        }

        public async Task<ResponseModel> UpdateDesignationAsync(DesignationsModel designation, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Designation_UpdateDesignation;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    designation
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DesignationClient", "UpdateDesignationAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating designation.", ex);
            }
        }

        public async Task<ResponseModel> DeleteDesignationAsync(long id, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Designation_DeleteDesignation, id);
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
                Helper.Error.ErrorBLL.CreateErrorLog("DesignationClient", "DeleteDesignationAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting designation.", ex);
            }
        }
    }
}
