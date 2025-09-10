using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Users
{
    public interface IUserService
    {
        Task<ResponseModel> AddUser(RequestUserDto user, string clientId);
    }
}
