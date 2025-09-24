using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Users.UserAccessManagement
{
    public interface IUserAccessClient
    {
        Task<ResponseModel> AddToUserAccessAsync(UserAccessDto request, string clientId);
        Task<ResponseModel> UpdateUserAccessAsync(UserAccessDto request, string clientId);
        Task<ResponseModel> DeleteUserAccessAsync(string uIDFK, string clientId);
        Task<ResponseModel> GetUserTypesAsync(string clientId);
    }
}
