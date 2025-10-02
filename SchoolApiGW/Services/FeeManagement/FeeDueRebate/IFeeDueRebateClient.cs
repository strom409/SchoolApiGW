using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.FeeManagement.FeeDueRebate
{
    public interface IFeeDueRebateClient
    {
        Task<ResponseModel> AddFeeDueRebate(FeeDueRebateDTO request, string clientId);
        Task<ResponseModel> UpdateFeeDueRebate(FeeDueRebateDTO request, string clientId);
        Task<ResponseModel> GetFeeDueRebateByStudentName(string studentName, string clientId);
        Task<ResponseModel> GetFeeDueRebateByAdmissionNo(string param, string clientId);
        Task<ResponseModel> GetFeeDueRebateByClassId(long classId, string clientId);
        Task<ResponseModel> DeleteFeeDueRebate(long rebateId, string clientId);
    }
}
