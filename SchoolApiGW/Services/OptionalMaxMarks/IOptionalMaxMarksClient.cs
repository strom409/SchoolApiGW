using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.OptionalMaxMarks
{
    public interface IOptionalMaxMarksClient
    {
        Task<ResponseModel> AddOptionalMaxMarks(OptionalMaxMarksDto dto, string clientId);
        Task<ResponseModel> UpdateOptionalMaxMarks(OptionalMaxMarksDto dto, string clientId);
        Task<ResponseModel> GetOptionalMaxMarksByFilter(string param, string clientId);
    }
}
