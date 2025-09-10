using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.MarksSheetSetting
{
    public interface IMarksSheetSettingClient
    {
        Task<ResponseModel> SaveMarksSheetSetting(MarksSheetSettingDto dto, string clientId);

    }
}
