using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.Marks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolApiGW.Services.MaxMarks
{
    public class MaxMarksClient : ProxyBaseUrl, IMaxMarksClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public MaxMarksClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddMaxMarks(MaxMarksDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.MaxMarks_Add;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    dto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MaxMarksClient", "AddMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding max marks.", ex);
            }
        }

        public async Task<ResponseModel> UpdateMaxMarks(MaxMarksDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.MaxMarks_Update;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    dto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MaxMarksClient", "UpdateMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating max marks.", ex);
            }
        }

        public async Task<ResponseModel> DeleteMaxMarks(string maxId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.MaxMarks_Delete, maxId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MaxMarksClient", "DeleteMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting max marks.", ex);
            }
        }

        public async Task<ResponseModel> GetAllMaxMarksByCurrentSession(string currentSession, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.MaxMarks_GetAllBySession, currentSession);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<MaxMarksDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MaxMarksClient", "GetAllMaxMarksByCurrentSession", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching max marks by session.", ex);
            }
        }

        public async Task<ResponseModel> GetMaxMarksByClassAndSubject(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.MaxMarks_GetByClassAndSubject, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<MaxMarksDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MaxMarksClient", "GetMaxMarksByClassAndSubject", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching max marks by class and subject.", ex);
            }
        }
    }
}

