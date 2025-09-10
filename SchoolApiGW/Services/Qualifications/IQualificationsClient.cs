using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Qualifications
{
    public interface IQualificationsClient
    {
        Task<ResponseModel> GetQualifications(string clientId);

        Task<ResponseModel> GetQualificationById(string qualificationId, string clientId);
        Task<ResponseModel> AddQualification(QualificationModel qualification, string clientId);

        Task<ResponseModel> UpdateQualification(QualificationModel qualification, string clientId);

        Task<ResponseModel> DeleteQualification(string qualificationId, string clientId);

    }
}
