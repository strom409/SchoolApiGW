using SchoolApiGW.Helper;
using System.Net.Http;

namespace SchoolApiGW.Services.Users
{
    public class UserClient : ProxyBaseUrl, IUserClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseModel> AddUserAsync(RequestUserDto user, string clientId)
        {
            var response = await ApiHelper.ApiConnection<ResponseModel>(
                _httpClientFactory,                             // Pass the factory
                user_Universal_API_Host,                        // Base URL from ProxyBaseUrl
                ProxyConstant.Clientuseradd_adduser,           // Endpoint
                HttpMethod.Post,                               // Method
                user                                            // Body
            );

            return response;
        }
    }

}
