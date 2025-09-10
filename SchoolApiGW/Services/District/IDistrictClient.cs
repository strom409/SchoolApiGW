using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.District
{
    public interface IDistrictClient
    {
        Task<ResponseModel> GetAllDistricts(string clientId);
        Task<ResponseModel> GetDistrictsByStateId(int stateId, string clientId);
        Task<ResponseModel> GetAllStates(string clientId);
    }
}
