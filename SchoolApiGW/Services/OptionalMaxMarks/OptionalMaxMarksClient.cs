using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.OptionalMarks;

namespace SchoolApiGW.Services.OptionalMaxMarks
{
    public class OptionalMaxMarksClient : ProxyBaseUrl, IOptionalMaxMarksClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public OptionalMaxMarksClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddOptionalMaxMarks(OptionalMaxMarksDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.OptionalMaxMarks_Add;
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
                Helper.Error.ErrorBLL.CreateErrorLog("OptionalMaxMarksClient", "AddOptionalMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding optional max marks.", ex);
            }
        }

        public async Task<ResponseModel> UpdateOptionalMaxMarks(OptionalMaxMarksDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.OptionalMaxMarks_Update;
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
                Helper.Error.ErrorBLL.CreateErrorLog("OptionalMaxMarksClient", "UpdateOptionalMaxMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating optional max marks.", ex);
            }
        }

        public async Task<ResponseModel> GetOptionalMaxMarksByFilter(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.OptionalMaxMarks_GetByFilter, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<OptionalMaxMarksDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("OptionalMaxMarksClient", "GetOptionalMaxMarksByFilter", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching optional max marks.", ex);
            }
        }
    }
}

