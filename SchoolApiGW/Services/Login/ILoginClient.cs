using SchoolApiGW.Helper;
using SchoolApiGW.Services.Users;

namespace SchoolApiGW.Services.login
{
    public interface ILoginClient
    {
        Task<ResponseModel> LoginUser(LoginDto request);
    }
}
