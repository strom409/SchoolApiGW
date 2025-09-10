using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Designation
{
    public interface IDesignationClient
    {
        Task<ResponseModel> GetDesignations(string clientId);
        Task<ResponseModel> GetDesignationById(long id, string clientId);
        Task<ResponseModel> AddDesignationAsync(DesignationsModel designation, string clientId);
        Task<ResponseModel> UpdateDesignationAsync(DesignationsModel designation, string clientId);
        Task<ResponseModel> DeleteDesignationAsync(long id, string clientId);

    }
}
