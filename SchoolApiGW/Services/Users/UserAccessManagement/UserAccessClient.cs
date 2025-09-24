using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.Subjects;

namespace SchoolApiGW.Services.Users.UserAccessManagement
{
    public class UserAccessClient : ProxyBaseUrl, IUserAccessClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserAccessClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddToUserAccessAsync(UserAccessDto request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.UserAccess_Add;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UserAccessClient", "AddToUserAccessAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding UserAccess.", ex);
            }
        }

        public async Task<ResponseModel> UpdateUserAccessAsync(UserAccessDto request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.UserAccess_Update;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UserAccessClient", "UpdateUserAccessAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating UserAccess.", ex);
            }
        }

        public async Task<ResponseModel> DeleteUserAccessAsync(string uIDFK, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.UserAccess_Delete + $"?uIDFK={uIDFK}";

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UserAccessClient", "DeleteUserAccessAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting UserAccess.", ex);
            }
        }

        public async Task<ResponseModel> GetUserTypesAsync(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.UserTypes_GetUserTypes;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host,   // base host for user-related APIs
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<UserTypeDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UserTypesClient", "GetUserTypesAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching user types.", ex);
            }
        }

    }
}
