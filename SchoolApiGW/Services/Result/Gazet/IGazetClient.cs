using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Result.Gazet
{
    public interface IGazetClient
    {
        Task<ResponseModel> GetGazetResults(string param, string clientId);
    }
}
