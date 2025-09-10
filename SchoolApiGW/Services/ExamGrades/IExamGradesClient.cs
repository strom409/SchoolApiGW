using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.ExamGrades
{
    public interface IExamGradesClient
    {
        Task<ResponseModel> GetExamGrades(string clientId);
        Task<ResponseModel> GetExamGradeById(long id, string clientId);
        Task<ResponseModel> AddExamGradeAsync(ExamGradesDTO grade, string clientId);
        Task<ResponseModel> UpdateExamGradeAsync(ExamGradesDTO grade, string clientId);
        Task<ResponseModel> DeleteExamGradeAsync(long id, string clientId);
    }
}
