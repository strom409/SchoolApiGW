using Azure.Core;
using Newtonsoft.Json;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Result.Gazet
{
    public class GazetClient : ProxyBaseUrl, IGazetClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public GazetClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> GetGazetResults(string param, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ExamGrades_GetGazetResults; // Define in ProxyConstant

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    param
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var results = JsonConvert.DeserializeObject<List<GazetResultDto>>(response.ResponseData.ToString());
                    response.ResponseData = results;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("GazetResultsClient", "GetGazetResultsAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching Gazet results.", ex);
            }
        }
    }
}


