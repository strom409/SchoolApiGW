using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Users
{
    public interface IUserClient
    {
        Task<ResponseModel> AddUserAsync(RequestUserDto user, string clientId);
    }
}
