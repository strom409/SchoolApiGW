using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.MaxMarks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolApiGW.Services.OptionalMarks
{
    public class OptionalMarksClient : ProxyBaseUrl, IOptionalMarksClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public OptionalMarksClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddOptionalMark(OptionalMarksDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.OptionalMarks_Add;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    dto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("OptionalMarksClient", "AddOptionalMark", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding optional mark.", ex);
            }
        }

        public async Task<ResponseModel> UpdateOptionalMarks(OptionalMarksDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.OptionalMarks_Update;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    dto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("OptionalMarksClient", "UpdateOptionalMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating optional marks.", ex);
            }
        }

        public async Task<ResponseModel> DeleteOptionalMarks(string id, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.OptionalMarks_Delete, id);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("OptionalMarksClient", "DeleteOptionalMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting optional marks.", ex);
            }
        }

        public async Task<ResponseModel> GetMaxMarksByClassSectionSubjectUnit(string id, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.OptionalMarks_GetByClassSectionSubjectUnit, id);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<OptionalMarksDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("OptionalMarksClient", "GetMaxMarksByClassSectionSubjectUnit", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching optional marks.", ex);
            }
        }
    }
}
    

