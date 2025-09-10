using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.EmpStatus;

namespace SchoolApiGW.Services.ExamGrades
{
    public class ExamGradesClient : ProxyBaseUrl, IExamGradesClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ExamGradesClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddExamGradeAsync(ExamGradesDTO grade, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ExamGrades_Add;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    grade
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesClient", "AddExamGradeAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding exam grade.", ex);
            }
        }

        public async Task<ResponseModel> UpdateExamGradeAsync(ExamGradesDTO grade, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ExamGrades_Update;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    grade
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesClient", "UpdateExamGradeAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating exam grade.", ex);
            }
        }
       
        public async Task<ResponseModel> GetExamGradeById(long id, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ExamGrades_GetById, id);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var grade = JsonConvert.DeserializeObject<ExamGradesDTO>(response.ResponseData.ToString());
                    response.ResponseData = grade;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesClient", "GetExamGradeById", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching exam grade with ID: {id}.", ex);
            }
        }

        public async Task<ResponseModel> GetExamGrades(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ExamGrades_GetAll;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var grades = JsonConvert.DeserializeObject<List<ExamGradesDTO>>(response.ResponseData.ToString());
                    response.ResponseData = grades;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesClient", "GetExamGrades", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching all exam grades.", ex);
            }
        }
        
        public async Task<ResponseModel> DeleteExamGradeAsync(long id, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ExamGrades_Delete, id);

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
                Helper.Error.ErrorBLL.CreateErrorLog("ExamGradesClient", "DeleteExamGradeAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while deleting exam grade with ID: {id}.", ex);
            }
        }

    }
}
