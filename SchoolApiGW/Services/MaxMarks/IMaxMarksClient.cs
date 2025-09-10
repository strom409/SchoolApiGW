using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.MaxMarks
{
    public interface IMaxMarksClient
    {
        Task<ResponseModel> GetAllMaxMarksByCurrentSession(string currentSession, string clientId);
        Task<ResponseModel> GetMaxMarksByClassAndSubject(string param, string clientId);
        Task<ResponseModel> AddMaxMarks(MaxMarksDto dto, string clientId);
        Task<ResponseModel> UpdateMaxMarks(MaxMarksDto dto, string clientId);
        Task<ResponseModel> DeleteMaxMarks(string maxId, string clientId);
    }
}
