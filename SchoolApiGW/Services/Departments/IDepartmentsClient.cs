using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Departments
{
    public interface IDepartmentsClient
    {
        Task<ResponseModel> AddDepartment(SubDepartment department, string clientId);
        Task<ResponseModel> UpdateDepartment(SubDepartment department, string clientId);
        Task<ResponseModel> DeleteDepartment(long id, string clientId);
        Task<ResponseModel> getDepartments(string clientId);
        Task<ResponseModel> GetDepartmentById(long id, string clientId);
    }
}
