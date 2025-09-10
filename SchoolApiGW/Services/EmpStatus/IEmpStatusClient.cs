using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.EmpStatus
{
    public interface IEmpStatusClient
    {
        Task<ResponseModel> GetEmployeeStatus(string clientId);
    }
}
