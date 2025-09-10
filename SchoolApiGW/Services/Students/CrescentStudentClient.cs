using Newtonsoft.Json;
using SchoolApiGW.Helper;
using System.Net.Http;
using System.Text;

namespace SchoolApiGW.Services.Students
{
    public class CrescentStudentClient : ProxyBaseUrl, ICrescentStudentClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public CrescentStudentClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddNewStudentWithUID(AddStudentRequestDTO request, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.Clientstudentpost_AddNewStudentWithUID;

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host, // base URL like http://localhost:59326/
                    formattedEndpoint,
                    HttpMethod.Post,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "AddNewStudentWithUID", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding a new student with UID.", ex);
            }
        }

        public async Task<ResponseModel> GetAllActiveStudentsOnSectionIDCrescentSchool(string sectionID, string clientId)
        {
            try
            {
                #region Format Endpoint
                // Assuming actionType=1 for this call; sectionID passed as query param
                string formattedEndpoint = string.Format(ProxyConstant.Clientstudent_getallactivestudentsonsectionidcrescent, 0, sectionID);

                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllActiveStudentsOnSectionIDCrescentSchool", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving active students by SectionID.", ex);
            }
        }

        public async Task<ResponseModel> GetAllDischargedStudentsOnSectionIDCrescentSchool(string sectionID, string clientId)
        {
            try
            {
                #region Format Endpoint
                // Assuming actionType=2 for this operation
                string formattedEndpoint = string.Format(
    ProxyConstant.Clientstudent_getalldischargedstudentsonsectionidcrescent, 1, sectionID);

                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllDischargedStudentsOnSectionIDCrescentSchool", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving discharged students by SectionID.", ex);
            }
        }

        public async Task<ResponseModel> GetAllStudentsOnSectionIDCrescent(string sectionId, string clientId)
        {
            try
            {
                #region Format Endpoint
                // Assuming actionType = 4 as per your microservice pattern
                string formattedEndpoint = string.Format(
    ProxyConstant.Clientstudent_getallstudentsonsectionidcrescent, 2, sectionId);

                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllStudentsOnSectionIDCrescent", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving students by SectionID.", ex);
            }
        }

        public async Task<ResponseModel> GetAllStudentsOnClassIDCrescent(string classId, string clientId)
        {
            try
            {
                #region Format Endpoint
                // Assuming actionType = 3 as per your pattern
                string formattedEndpoint = string.Format(
     ProxyConstant.Clientstudent_getallstudentsonclassidcrescent, 3, classId);

                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllStudentsOnClassIDCrescent", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving students by ClassID.", ex);
            }
        }

        public async Task<ResponseModel> GetInvalidDischargeListOnClassIDCrescent(string classId, string clientId)
        {
            try
            {
                #region Format Endpoint
                // Assuming actionType = 5 is standard for this call
                string formattedEndpoint = string.Format(
     ProxyConstant.Clientstudent_getinvaliddischargelistonclassidcrescent, 4, classId);

                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetInvalidDischargeListOnClassIDCrescent", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving invalid discharge student list by ClassID.", ex);
            }
        }

        public async Task<ResponseModel> GetMaxUID(string currentSession, string clientId)
        {
            try
            {
                #region Format Endpoint
                // Assuming actionType = 5 and currentSession is passed as query parameter
                string formattedEndpoint = string.Format(
    ProxyConstant.Clientstudent_getmaxuid, 5, currentSession);

                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetMaxUID", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving Max UID.", ex);
            }
        }

        public async Task<ResponseModel> GetMaxRollNo(string classId, string sectionId, string clientId)
        {
            try
            {
                #region Format Endpoint
                // Assuming actionType = 6
                string formattedEndpoint = string.Format(
     ProxyConstant.Clientstudent_getmaxrollno, 6, classId, sectionId);


                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetMaxRollNo", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving max roll number.", ex);
            }
        }

        public async Task<ResponseModel> GetActiveStudentsOnUID(string UID, string clientId)
        {
            try
            {
                #region Format Endpoint
                string formattedEndpoint = string.Format(ProxyConstant.Clientstudent_getactivestudentsonuid, 7, UID);
                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetActiveStudentsOnUID", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving active students by UID.", ex);
            }
        }

       public async Task<ResponseModel> GetStudentsByPhoneNumber(string phoneNo, string clientId)
{
    try
    {
        #region Format Endpoint
        string formattedEndpoint = string.Format(
            ProxyConstant.Clientstudent_getstudentsbyphoneno, 8, phoneNo);
        #endregion

        #region API Call
        var response = await ApiHelper.ApiConnection<ResponseModel>(
            _httpClientFactory,
            student_Universal_API_Host,
            formattedEndpoint,
            HttpMethod.Get,
            null);
        #endregion

        #region Deserialize Result
        if (response.IsSuccess && response.ResponseData != null)
        {
            var studentList = JsonConvert.DeserializeObject<StudentDTO>(
                response.ResponseData.ToString());

            response.ResponseData = studentList;
        }
        #endregion

        return response;
    }
    catch (Exception ex)
    {
        #region Error Logging
        Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentsByPhoneNumber", ex.Message + " | " + ex.StackTrace);
        #endregion

        throw new ApplicationException("Error occurred while retrieving student by phone number.", ex);
    }
}

        public async Task<ResponseModel> GetStudentsByAddress(string address, string clientId)
        {
            try
            {
                #region Format Endpoint
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudent_getstudentsbyaddress, 9, Uri.EscapeDataString(address));
                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentsByAddress", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving student by address.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentsByName(string name, string clientId)
        {
            try
            {
                #region Format Endpoint
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudent_getstudentsbyname, 10, Uri.EscapeDataString(name));
                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentsByName", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving student by name.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentsByAcademicNo(string academicNo, string clientId)
        {
            try
            {
                #region Format Endpoint
                // Assuming actionType = 6 for this functionality (you can adjust this)
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudent_getstudentsbyacademicno, 11, academicNo);
                #endregion

                #region API Call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);
                #endregion

                #region Deserialize Result
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                        response.ResponseData.ToString());

                    response.ResponseData = studentList;
                }
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                #region Error Logging
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentsByAcademicNo", ex.Message + " | " + ex.StackTrace);
                #endregion

                throw new ApplicationException("Error occurred while retrieving student by Academic No.", ex);
            }
        }
        #endregion
    }

}


