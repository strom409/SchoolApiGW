using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.HT
{
    public interface IHTClient
    {
        Task<ResponseModel> getHT(string clientId);
        Task<ResponseModel> UpdateHT(HTModel htData, string clientId);
    }
}
