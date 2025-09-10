using SchoolApiGW.Helper;
using SchoolApiGW.Services.login;

namespace SchoolApiGW.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserClient _userClient;

        // Inject IUserClient, not IUserService
        public UserService(IUserClient userClient)
        {
            _userClient = userClient;
        }

        public async Task<ResponseModel> AddUser(RequestUserDto user, string clientId)
        {
            return await _userClient.AddUserAsync(user, clientId);
        }
    }
}
