using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchoolApiGW.Helper;
using System.Net.Http;

namespace SchoolApiGW.Services.Users
{
    public class UserClient : ProxyBaseUrl, IUserClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClient(IConfiguration configuration, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _env = env; 
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor= httpContextAccessor;
        }

        public async Task<ResponseModel> AddUserAsync(RequestUserDto request, string clientId)
        {
            try
            {
                // 1️⃣ Send user data to microservice (without photo)
                string formattedEndpoint = ProxyConstant.Clientuserpost_PostAddUser;

                var addResponse = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    request // request without the photo
                );

                if (!addResponse.IsSuccess || addResponse.ResponseData == null)
                    return addResponse;

                // 2️⃣ Get UserID from microservice response
                string userId = null;
                if (addResponse.ResponseData != null)
                {
                    string json = JsonConvert.SerializeObject(addResponse.ResponseData);
                    var jObj = JsonConvert.DeserializeObject<JObject>(json);
                    userId = jObj?["userId"]?.ToString();
                }

                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Status = 0,
                        Message = "Failed to retrieve UserID from microservice."
                    };
                }

                // 3️⃣ Save User Photo in folder (no DB update needed)
                if (request.UserPhotoFile != null && request.UserPhotoFile.Length > 0)
                {
                    string userPhotoUrl = await SavePhotoAsync(request.UserPhotoFile, clientId, userId, "UserPhoto");
                    request.UserPhoto = userPhotoUrl;       // store URL in DTO
                    request.UserPhotoFile = null;           // remove file from memory
                }

                return addResponse;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UserClient", "AddUserAsync", ex.Message + " | " + ex.StackTrace);
                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
        public async Task<ResponseModel> UpdateUserAsync(RequestUserDto user, string clientId)
        {
            try
            {
                // 1️⃣ Ensure UserId is provided
                if (string.IsNullOrWhiteSpace(user.UserId))
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Status = 0,
                        Message = "UserId is required for update!"
                    };
                }

                // 2️⃣ Save User Photo if provided
                if (user.UserPhotoFile != null)
                {
                    string userPhotoUrl = await SavePhotoAsync(user.UserPhotoFile, clientId, user.UserId, "UserPhoto");
                    user.UserPhoto = userPhotoUrl; // store URL
                    user.UserPhotoFile = null;     // optional, clear file
                }

                // 3️⃣ Call microservice via proxy
                string formattedEndpoint = string.Format(ProxyConstant.Clientuserput_PutUpdateUser, 0); // actionType = 1
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    user
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "UserClient",
                    "UpdateUserAsync",
                    ex.Message + " | " + ex.StackTrace
                );

                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> DeleteUserAsync(int userId, string clientId)
        {
            try
            {
                if (userId <= 0)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Status = 0,
                        Message = "Valid UserId is required to delete a user."
                    };
                }

                // Build endpoint with actionType if needed
                string formattedEndpoint = string.Format(ProxyConstant.Clientuserdelete_DeleteUser, userId);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    user_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Delete,
                    null // No body needed for delete
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "UserClient",
                    "DeleteUserAsync",
                    ex.Message + " | " + ex.StackTrace
                );

                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Message = $"Error: {ex.Message}"
                };
            }
        }



        private async Task<string?> SavePhotoAsync(IFormFile photo, string clientId, string userId, string folder)
        {
            if (photo == null || photo.Length == 0)
                return null;

            try
            {
                // Build folder path dynamically
                string photoRoot = Path.Combine(_env.ContentRootPath, "ClientData", clientId, "Users", userId, folder);
                if (!Directory.Exists(photoRoot))
                    Directory.CreateDirectory(photoRoot);

                // ✅ Remove any existing files (keep only one image per user)
                foreach (var existingFile in Directory.GetFiles(photoRoot))
                {
                    File.Delete(existingFile);
                }

                // ✅ Use a consistent file name (like UserID + extension)
                string fileName = $"{userId}{Path.GetExtension(photo.FileName)}";
                string fullPath = Path.Combine(photoRoot, fileName);

                // Save file
                await using var stream = new FileStream(fullPath, FileMode.Create);
                await photo.CopyToAsync(stream);

                // Build accessible URL
                var httpRequest = _httpContextAccessor.HttpContext?.Request;
                string baseUrl = httpRequest != null ? $"{httpRequest.Scheme}://{httpRequest.Host}" : "";
                return $"{baseUrl}/ClientData/{clientId}/Users/{userId}/{folder}/{fileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving photo: {ex.Message}");
                return null;
            }
        }

    }

}
