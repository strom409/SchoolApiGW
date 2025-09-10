using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Helper.Error;
using SchoolApiGW.Services.Salary;

namespace SchoolApiGW.Services.District
{
    public class DistrictClient : ProxyBaseUrl, IDistrictClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public DistrictClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> GetAllDistricts(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.District_GetAll;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<DistrictDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                ErrorBLL.CreateErrorLog("DistrictClient", "GetAllDistricts", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching all districts.", ex);
            }
        }

        public async Task<ResponseModel> GetAllStates(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.State_GetAll;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<StateDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                ErrorBLL.CreateErrorLog("DistrictClient", "GetAllStates", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching all states.", ex);
            }
        }

        public async Task<ResponseModel> GetDistrictsByStateId(int stateId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.District_GetByStateId, stateId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<DistrictDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                ErrorBLL.CreateErrorLog("DistrictClient", "GetDistrictsByStateId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching districts by state ID.", ex);
            }
        }
    }
}
