using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.Employee;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolApiGW.Services.Marks
{
    public class MarksClient : ProxyBaseUrl, IMarksClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public MarksClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddMarks(MarksDto marksDto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Marks_AddMarks;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    marksDto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MarksClient", "AddMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding marks.", ex);
            }
        }

        public async Task<ResponseModel> UpdateMarks(MarksDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Marks_UpdateMarks;
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
                Helper.Error.ErrorBLL.CreateErrorLog("MarksClient", "UpdateMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating marks.", ex);
            }
        }

        public async Task<ResponseModel> DeleteMarks(string marksId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Marks_DeleteMarks, marksId);
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
                Helper.Error.ErrorBLL.CreateErrorLog("MarksClient", "DeleteMarks", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting marks.", ex);
            }
        }

        public async Task<ResponseModel> GetMarksWithNames(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Marks_GetMarksWithNames, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<MarksDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("MarksClient", "GetMarksWithNames", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching marks with names.", ex);
            }
        }
    }

}
