using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.Users;

namespace SchoolApiGW.Services.login
{
    public class LoginClient : ProxyBaseUrl, ILoginClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClient;

        public LoginClient(IConfiguration configuration, IHttpClientFactory httpClient) : base(configuration)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseModel> LoginUser(LoginDto request)
        {
            try
            {
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClient,
                    this.user_Universal_API_Host,
                    ProxyConstant.ClientLoginUser_LoginUser,
                    HttpMethod.Post,
                    request);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var loginDto = JsonConvert.DeserializeObject<UserDTO>(
                        response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = loginDto;
                }

                return response;
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = 0,
                    Message = "Error occurred during login",
                    Error = ex.Message
                };
            }
        }
    }
    }


