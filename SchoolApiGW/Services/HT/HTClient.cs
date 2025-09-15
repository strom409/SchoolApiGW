using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.EmpStatus;
using System.Net.Http;

namespace SchoolApiGW.Services.HT
{
    public class HTClient : ProxyBaseUrl, IHTClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public HTClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> getHT(string clientId)
        {
            try
            {

                // Format endpoint (adjust constant name to match your config)
                string formattedEndpoint = string.Format(ProxyConstant.HT_GetHT, clientId);

                // Call API
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host, 
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                // Deserialize response data if success
                if (response.IsSuccess && response.ResponseData != null)
                {

                    var ht = JsonConvert.DeserializeObject<HTModel>(response.ResponseData.ToString());
                    response.ResponseData = ht;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("HTClient", "getHT", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching HT list.", ex);
            }
        }


        public async Task<ResponseModel> UpdateHT(HTModel htData, string clientId)
        {
            try
            {
                // Format endpoint with clientId
                string formattedEndpoint = string.Format(ProxyConstant.HT_UpdateHT,clientId);

                // Call API with data
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host, 
                    formattedEndpoint,
                    HttpMethod.Put,        
                    htData                  
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("HTClient", "UpdateHT", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating HT.", ex);
            }
        }

    }
}
