using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.OptionalMarks
{
    public interface IOptionalMarksClient
    {
        Task<ResponseModel> AddOptionalMark(OptionalMarksDto dto, string clientId);
        Task<ResponseModel> UpdateOptionalMarks(OptionalMarksDto dto, string clientId);
        Task<ResponseModel> GetMaxMarksByClassSectionSubjectUnit(string id, string clientId);
        Task<ResponseModel> DeleteOptionalMarks(string id, string clientId);
    }
}
