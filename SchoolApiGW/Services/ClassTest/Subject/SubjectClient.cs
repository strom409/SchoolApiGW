using Microsoft.Extensions.Configuration;
using SchoolApiGW.Helper;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace SchoolApiGW.Services.ClassTest.Subject
{
    public class SubjectClient : ProxyBaseUrl, ISubjectClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public SubjectClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> InsertNewSubject(SubjectDTO subname, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Subject_InsertNewSubject;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    subname
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "InsertNewSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while inserting new subject.", ex);
            }
        }

        public async Task<ResponseModel> InsertNewOptionalSubject(SubjectDTO value, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Subject_InsertNewOptionalSubject;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    value
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "InsertNewOptionalSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while inserting new optional subject.", ex);
            }
        }

        public async Task<ResponseModel> InsertNewSubSubject(SubjectDTO value, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Subject_InsertNewSubSubject;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    value
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "InsertNewSubSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while inserting new sub subject.", ex);
            }
        }

        public async Task<ResponseModel> UpdateSubject(SubjectDTO value, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Subject_UpdateSubject;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    value
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "UpdateSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating subject.", ex);
            }
        }

        public async Task<ResponseModel> UpdateOptionalSubject(SubjectDTO value, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Subject_UpdateOptionalSubject;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    value
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "UpdateOptionalSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating optional subject.", ex);
            }
        }

        public async Task<ResponseModel> UpdateSubSubject(SubjectDTO value, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Subject_UpdateSubSubject;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    value
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "UpdateSubSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating sub subject.", ex);
            }
        }

        public async Task<ResponseModel> GetSubjectsByClassId(string? param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Subject_GetSubjectsByClassId, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<SubjectDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "GetSubjectsByClassId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting subjects by class id.", ex);
            }
        }

        public async Task<ResponseModel> GetOptionalSubjectsByClassId(string? param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Subject_GetOptionalSubjectsByClassId, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<SubjectDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "GetOptionalSubjectsByClassId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting optional subjects by class id.", ex);
            }
        }

        public async Task<ResponseModel> GetSubSubjectsBySubjectId(string? param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Subject_GetSubSubjectsBySubjectId, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<SubjectDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "GetSubSubjectsBySubjectId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting sub subjects by subject id.", ex);
            }
        }

        public async Task<ResponseModel> DeleteSubject(string subjectId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Subject_DeleteSubject, subjectId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "DeleteSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting subject.", ex);
            }
        }

        public async Task<ResponseModel> DeleteOptionalSubject(string optionalSubjectId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Subject_DeleteOptionalSubject, optionalSubjectId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "DeleteOptionalSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting optional subject.", ex);
            }
        }

        public async Task<ResponseModel> DeleteSubSubject(string subSubjectId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Subject_DeleteSubSubject, subSubjectId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectClient", "DeleteSubSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting sub subject.", ex);
            }
        }
    }
}
