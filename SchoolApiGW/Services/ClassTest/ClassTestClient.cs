using Microsoft.Extensions.Configuration;
using SchoolApiGW.Helper;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace SchoolApiGW.Services.ClassTest
{
    public class ClassTestClient : ProxyBaseUrl, IClassTestClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ClassTestClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddClassTestMaxMarks(List<ClassTestDTO> list, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassTest_AddClassTestMaxMarks;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    list
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "AddClassTestMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding class test max marks.", ex);
            }
        }

        public async Task<ResponseModel> AddClassTestMarks(List<ClassTestDTO> list, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassTest_AddClassTestMarks;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    list
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "AddClassTestMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding class test marks.", ex);
            }
        }

        public async Task<ResponseModel> UpdateClassTestMaxMarks(List<ClassTestDTO> list, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassTest_UpdateClassTestMaxMarks;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    list
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "UpdateClassTestMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating class test max marks.", ex);
            }
        }

        public async Task<ResponseModel> EditUpdateClassTestMarks(List<ClassTestDTO> list, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassTest_EditUpdateClassTestMarks;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    list
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "EditUpdateClassTestMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while editing/updating class test marks.", ex);
            }
        }

        public async Task<ResponseModel> GetSubjectForMaxMarks(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_GetSubjectForMaxMarks, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "GetSubjectForMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting subject for max marks.", ex);
            }
        }

        public async Task<ResponseModel> GetMaxMarks(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_GetMaxMarks, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "GetMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting max marks.", ex);
            }
        }

        public async Task<ResponseModel> GetStudents(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_GetStudents, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "GetStudents", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting students.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentsWithMarks(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_GetStudentsWithMarks, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "GetStudentsWithMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting students with marks.", ex);
            }
        }

        public async Task<ResponseModel> ViewDateWiseResult(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_ViewDateWiseResult, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "ViewDateWiseResult", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while viewing date wise result.", ex);
            }
        }

        public async Task<ResponseModel> ClassTestReport(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_ClassTestReport, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "ClassTestReport", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting class test report.", ex);
            }
        }

        public async Task<ResponseModel> ViewDateWiseResultForAllSubjects(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_ViewDateWiseResultForAllSubjects, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "ViewDateWiseResultForAllSubjects", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while viewing date wise result for all subjects.", ex);
            }
        }

        public async Task<ResponseModel> ViewDateWiseResultForTotalMarks(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_ViewDateWiseResultForTotalMarks, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "ViewDateWiseResultForTotalMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while viewing date wise result for total marks.", ex);
            }
        }

        public async Task<ResponseModel> ViewDateWiseTotalMMandObtMarks(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_ViewDateWiseTotalMMandObtMarks, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<ClassTestDTO>>(
                        response.ResponseData.ToString()
                    );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "ViewDateWiseTotalMMandObtMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while viewing date wise total MM and obtained marks.", ex);
            }
        }

        public async Task<ResponseModel> GetMissingClassTestMarks(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassTest_GetMissingClassTestMarks, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    classTest_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(
         response.ResponseData.ToString()
                     );
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassTestClient", "GetMissingClassTestMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting missing class test marks.", ex);
            }
        }
    }
}
