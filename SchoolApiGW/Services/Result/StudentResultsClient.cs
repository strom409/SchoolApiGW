using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SchoolApiGW.Helper;
using System.Net.Http;

namespace SchoolApiGW.Services.Result
{
    public class StudentResultsClient : ProxyBaseUrl, IStudentResultsClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentResultsClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> GetOptionalResultsAsync(OptionalResultsRequestDto request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ExamGrades_GetOptionalResults; // Add this in ProxyConstant

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    request
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var results = JsonConvert.DeserializeObject<List<OptionalStudentResultDto>>(response.ResponseData.ToString());
                    response.ResponseData = results;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesClient", "GetOptionalResultsAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching optional results.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentResultsAsync(StudentResultsRequestDto request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ExamGrades_GetStudentResults; 

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    request
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var results = JsonConvert.DeserializeObject<List<StudentResultDto>>(response.ResponseData.ToString());
                    response.ResponseData = results;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesClient", "GetStudentResultsAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching student results.", ex);
            }
        }
    }
}
