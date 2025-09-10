using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Students
{
    public interface IStudentClient
    {
        Task<ResponseModel> AddStudentAsync(AddStudentRequestDTO request, string clientId);
        Task<ResponseModel> GetStudentsByClassAsync(long classId, string clientId);
        Task<ResponseModel?> GetStudentByAdmissionNoAsync(string admissionNo, string clientId);
        Task<ResponseModel> GetStudentsByNameAsync(string studentName, string clientId);
        Task<ResponseModel> GetStudentByStudentInfoIdAsync(long studentInfoId, string clientId);
        Task<ResponseModel?> GetStudentByPhoneAsync(string phoneNo, string clientId);
        Task<ResponseModel> GetStudentsByCurrentSessionAsync(string currentSession, string clientId);
        Task<ResponseModel> GetAllStudentsOnSectionIDAsync(string sectionId, string clientId);
        Task<ResponseModel> GetOnlyActiveStudentsOnClassIDAsync(long classId, string clientId);
        Task<ResponseModel> GetOnlyActiveStudentsOnSectionIDAsync(long sectionId, string clientId);
        Task<ResponseModel> GetMaxRollnoAsync(string sectionId, string clientId);
        Task<ResponseModel> GetAllStudentsOnClassIDAsync(string classId, string clientId);
        Task<ResponseModel> GetAllDischargedStudentsOnSectionIDAsync(string sectionId, string clientId);
        Task<ResponseModel> TotalStudentsRollForDashBoardAsync(string session, string clientId);
        Task<ResponseModel> ClassWisStudentsRollForDashBoardAsync(string session, string clientId);
        Task<ResponseModel> TotalStudentsRollForDashBoardOnDateAsync(string session, string clientId);
        Task<ResponseModel> SectionWisStudentsRollWithAttendanceForDashBoardAsync(string ClassID, string clientId);
        Task<ResponseModel> GetAllStudentsOnStudentNameAndCSessionAsync(string NameAndsession, string clientId);
        Task<ResponseModel> GetBoardNoWithDateAsync(string classSectionId, string clientId);
        Task<ResponseModel> GetAllStudentsOnSessionAsync(string session, string clientId);
        Task<ResponseModel> GetNextAdmissionNoAsync(string clientId);
        Task<ResponseModel> GetAllSessions(string clientId);
        Task<ResponseModel> UpdateStudentAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> UpdateParentDetailAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> UpdateAddressDetailAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> UpdatePersonalDetailAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> UpdateStudentRollNoAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> UpdateClassStudentRollNumbers(List<StudentRollNoUpdate> updates, string clientId);
        Task<ResponseModel> UpdateBoardNoAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> UpdateDOBAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> UpdateSectionAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> UpdateClassAsync(UpdateStudentRequestDTO request, string clientId);

        Task<ResponseModel> DischargeStudentAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> DischargeStudentForIntValueAsync(UpdateStudentRequestDTO request, string clientId);
        Task<ResponseModel> RejoinStudentAsync(UpdateStudentRequestDTO request, string clientId);

        Task<ResponseModel> RejoinStudentForIntValueAsync(UpdateStudentRequestDTO request, string clientId);

        Task<ResponseModel> UpdateStudentEducationAdmissionPrePrimaryEtcAsync(UpdateStudentRequestDTO request, string clientId);

        Task<ResponseModel> UpdateStudentHeightWeightAdharNamePENEtcUDISEAsync(UpdateStudentRequestDTO request, string clientId);

        Task<ResponseModel> UpdateStudentSessionAsync(StudentSessionUpdateRequest request, string clientId);
    }
}

