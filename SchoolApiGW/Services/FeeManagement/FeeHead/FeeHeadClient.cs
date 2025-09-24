using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.FeeManagement.FeeStructure;

namespace SchoolApiGW.Services.FeeManagement.FeeHead
{
    public class FeeHeadClient : ProxyBaseUrl, IFeeHeadClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FeeHeadClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddFeeHead(FeeHeadDto request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.FeeHead_AddFeeHead;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    request
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeHeadClient", "AddFeeHead", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding fee head.", ex);
            }
        }

        public async Task<ResponseModel> UpdateFeeHead(FeeHeadDto request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.FeeHead_UpdateFeeHead;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    request
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeHeadClient", "UpdateFeeHead", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating fee head.", ex);
            }
        }

        public async Task<ResponseModel> DeleteFeeHead(long fHID, string clientId, string updatedBy)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeHead_DeleteFeeHead, fHID, updatedBy);
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
                Helper.Error.ErrorBLL.CreateErrorLog("FeeHeadClient", "DeleteFeeHead", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting fee head.", ex);
            }
        }

        public async Task<ResponseModel> GetFeeHeadById(long fHID, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeHead_GetFeeHeadById, fHID);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var dto = JsonConvert.DeserializeObject<FeeHeadDto>(response.ResponseData.ToString());
                    response.ResponseData = dto;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeHeadClient", "GetFeeHeadById", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching fee head by ID: {fHID}.", ex);
            }
        }

        public async Task<ResponseModel> GetAllFeeHeads(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.FeeHead_GetAllFeeHeads;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<FeeHeadDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeHeadClient", "GetAllFeeHeads", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching fee heads.", ex);
            }
        }

        public async Task<ResponseModel> GetFeeHeadsByType(int fHType, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeHead_GetFeeHeadsByType, fHType);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<FeeHeadDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeHeadClient", "GetFeeHeadsByType", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching fee heads by type: {fHType}.", ex);
            }
        }
    }
}
