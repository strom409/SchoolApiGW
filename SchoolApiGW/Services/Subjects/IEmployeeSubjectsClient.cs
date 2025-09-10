using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Subjects
{
    public interface IEmployeeSubjectsClient
    {
        Task<ResponseModel> GetEmployeeSubjects(string clientId);
        Task<ResponseModel> GetEmployeeSubjectById(string ESID, string clientId);
    }
}
