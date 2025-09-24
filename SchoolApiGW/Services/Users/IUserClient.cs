using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Users
{
    public interface IUserClient
    {
        Task<ResponseModel> AddUserAsync(RequestUserDto user, string clientId);
        Task<ResponseModel> UpdateUserAsync(RequestUserDto user, string clientId);
        Task<ResponseModel> ChangeUserPasswordAsync(RequestUserDto request, string clientId);
        Task<ResponseModel> DeleteUserAsync(int userId, string clientId);
    }
}
