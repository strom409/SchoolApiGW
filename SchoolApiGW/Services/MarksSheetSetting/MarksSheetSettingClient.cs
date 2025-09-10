using SchoolApiGW.Helper;
using SchoolApiGW.Services.Marks;

namespace SchoolApiGW.Services.MarksSheetSetting
{
    public class MarksSheetSettingClient : ProxyBaseUrl, IMarksSheetSettingClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public MarksSheetSettingClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> SaveMarksSheetSetting(MarksSheetSettingDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.MarksSheetSetting_SaveMarksSheetSetting;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put, // Since it's an update
                    dto
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MarksClient", "SaveMarksSheetSetting", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while saving mark sheet setting.", ex);
            }
        }

    }
}
