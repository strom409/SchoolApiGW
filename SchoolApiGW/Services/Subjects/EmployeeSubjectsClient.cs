using Newtonsoft.Json;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Subjects
{
    public class EmployeeSubjectsClient : ProxyBaseUrl, IEmployeeSubjectsClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeeSubjectsClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<ResponseModel> GetEmployeeSubjects(string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeeSubjects);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var subjectList = JsonConvert.DeserializeObject<List<EmployeeSubjects>>(response.ResponseData.ToString());
                    response.ResponseData = subjectList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectsClient", "GetEmployeeSubjects", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching employee subjects.", ex);
            }
        }
        public async Task<ResponseModel> GetEmployeeSubjectById(string ESID, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeeSubjectById, ESID);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var subject = JsonConvert.DeserializeObject<EmployeeSubjects>(response.ResponseData.ToString());
                    response.ResponseData = subject;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("SubjectsClient", "GetEmployeeSubjectById", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching Employee Subject by ID: {ESID}.", ex);
            }
        }


    }
}
