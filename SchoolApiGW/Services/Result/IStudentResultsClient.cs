using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Result
{
    public interface IStudentResultsClient
    {
        Task<ResponseModel> GetStudentResultsAsync(StudentResultsRequestDto request, string clientId);
        Task<ResponseModel> GetOptionalResultsAsync(OptionalResultsRequestDto request, string clientId);
    }
}
