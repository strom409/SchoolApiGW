using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.FeeManagement.FeeHead;

namespace SchoolApiGW.Services.FeeManagement.FeeDue
{
    public class FeeDueClient : ProxyBaseUrl, IFeeDueClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FeeDueClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddFeeDue(FeeDueDTO request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.FeeDue_AddFeeDue;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    request
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueClient", "AddFeeDue", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding FeeDue.", ex);
            }
        }

        public async Task<ResponseModel> DeleteFeeDue(long feeDueID, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeDue_DeleteFeeDue, feeDueID);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueClient", "DeleteFeeDue", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting FeeDue.", ex);
            }
        }

        public async Task<ResponseModel> GetAllMonths(string clientId)
        {
            try
            {
                // Assuming your proxy constant for months is something like:
                // public const string Month_GetAll = "/api/Month/fetch?actionType=0";
                string endpoint = ProxyConstant.Month_GetAll;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var monthsList = JsonConvert.DeserializeObject<List<SessionMonthDTO>>(response.ResponseData.ToString());
                    response.ResponseData = monthsList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MonthClient", "GetAllMonths", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching months.", ex);
            }
        }


        public async Task<ResponseModel> GetFeeDueByAdmissionNo(string param, string clientId)
        {
            try
            {
                // Format endpoint with full param (AdmissionNo,CurrentSession)
                string endpoint = string.Format(
                    ProxyConstant.FeeDue_GetByAdmissionNo,
                    param
                );

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                // Deserialize response data into DTO list
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var dto = JsonConvert.DeserializeObject<List<FeeDueDTO>>(response.ResponseData.ToString());
                    response.ResponseData = dto;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "FeeDueClient",
                    "GetFeeDueByAdmissionNo",
                    $"Error: {ex.Message} | StackTrace: {ex.StackTrace} | Param: {param}"
                );
                throw new ApplicationException(
                    $"Error occurred while fetching FeeDue for param: {param}.",
                    ex
                );
            }
        }


        public async Task<ResponseModel> GetFeeDueByClassId(long classId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeDue_GetByClassId, classId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<FeeDueDTO>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueClient", "GetFeeDueByClassId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching FeeDue for ClassID: {classId}.", ex);
            }
        }

        public async Task<ResponseModel> GetFeeDueByStudentName(string studentName, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeDue_GetByStudentName, studentName);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<FeeDueDTO>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueClient", "GetFeeDueByStudentName", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching FeeDue for StudentName: {studentName}.", ex);
            }
        }

        public Task<ResponseModel> UpdateFeeDue(FeeDueDTO request, string clientId)
        {
            throw new NotImplementedException();
        }
    }
}

