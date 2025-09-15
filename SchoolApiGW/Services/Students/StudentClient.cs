using Azure;
using Azure.Core;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.ClassMaster;
using SchoolApiGW.Services.Users;
using System.Net.Http;
using System.Text.Json;
using static System.Collections.Specialized.BitVector32;

namespace SchoolApiGW.Services.Students
{
    public class StudentClient : ProxyBaseUrl, IStudentClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentClient(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        public async Task<ResponseModel> AddStudentAsync(AddStudentRequestDTO request, string clientId)
        {
            try
            {
                // 1️⃣ Send student data to microservice (without photos)
                string formattedEndpoint = ProxyConstant.Clientstudentpost_PostAddStudent;

                var addResponse = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    request // request without actual files
                );

                if (!addResponse.IsSuccess || addResponse.ResponseData == null)
                    return addResponse;

                // 2️⃣ Get StudentID from microservice response
                string studentId = null;
                if (addResponse.ResponseData != null)
                {
                    var jObj = addResponse.ResponseData as JObject;
                    studentId = jObj?["studentID"]?.ToString();
                }

                if (string.IsNullOrEmpty(studentId))
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Status = 0,
                        Message = "Failed to retrieve StudentID from microservice."
                    };
                }



                if (string.IsNullOrEmpty(studentId))
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Status = 0,
                        Message = "Failed to retrieve StudentID from microservice."
                    };
                }

                // 3️⃣ Save photos in folders (no DB update needed)
                if (request.StudentPhoto != null && request.StudentPhoto.Length > 0)
                {
                    await SavePhotoAsync(request.StudentPhoto, clientId, studentId, "StudentPhoto");
                    request.StudentPhoto = null;
                }

                if (request.FatherPhoto != null && request.FatherPhoto.Length > 0)
                {
                    await SavePhotoAsync(request.FatherPhoto, clientId, studentId, "FatherPhoto");
                    request.FatherPhoto = null;
                }

                if (request.MotherPhoto != null && request.MotherPhoto.Length > 0)
                {
                    await SavePhotoAsync(request.MotherPhoto, clientId, studentId, "MotherPhoto");
                    request.MotherPhoto = null;
                }

                return addResponse;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "AddStudentAsync", ex.Message + " | " + ex.StackTrace);
                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Message = $"Error: {ex.Message}"
                };
            }
        }



        public async Task<ResponseModel> GetStudentsByClassAsync(long classId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetbyclass_getstudentsbyclass, 0, classId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentList = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentsByClassAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by class ID: {classId}.", ex);
            }
        }

        public async Task<ResponseModel?> GetStudentByAdmissionNoAsync(string admissionNo, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetbyadmissionno_getstudentbyadmissionno, 1, admissionNo);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var student = JsonConvert.DeserializeObject<StudentDTO>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = student;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentByAdmissionNoAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by AdmissionNo: {admissionNo}.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentsByNameAsync(string studentName, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetbyname_getstudentsbyname, 2, studentName);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentsByNameAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by Name: {studentName}.", ex);
            }
        }
        public async Task<ResponseModel> GetStudentByStudentInfoIdAsync(long studentInfoId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetbystudentinfoid_getstudentbystudentinfoid, 3, studentInfoId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var student = JsonConvert.DeserializeObject<StudentDTO>(
                        response.ResponseData.ToString()
                    );

                    if (student != null && int.TryParse(student.StudentID, out int studentIdInt))
                    {
                        // Re-map photo paths based on Gateway's folder structure
                        student.FatherPhotoPath = GetPhotoPath(clientId, studentIdInt, "Father");
                        student.MotherPhotoPath = GetPhotoPath(clientId, studentIdInt, "Mother");
                        student.PhotoPath = GetPhotoPath(clientId, studentIdInt, "Student");
                    }

                    response.ResponseData = student; // attach updated DTO
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentByStudentInfoIdAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by StudentInfoId: {studentInfoId}.", ex);
            }
        }

        // Same GetPhotoPath from microservice, but inside Gateway
        private string? GetPhotoPath(string clientId, int studentId, string photoType)
        {
            string folder = photoType switch
            {
                "Student" => "StudentPhoto",
                "Father" => "FatherPhoto",
                "Mother" => "MotherPhoto",
                _ => throw new ArgumentException("Invalid photoType")
            };

            // Use the application root folder dynamically
            string rootPath = Path.Combine(_env.ContentRootPath, "ClientData");

            string basePath = Path.Combine(rootPath, clientId, studentId.ToString(), folder);

            if (!Directory.Exists(basePath)) return null;

            var files = Directory.GetFiles(basePath)
                                 .OrderByDescending(f => File.GetCreationTime(f))
                                 .ToList();

            if (files.Count == 0) return null;
            string relativePath = $"/ClientData/{clientId}/{studentId}/{folder}/{Path.GetFileName(files.First())}";

            // Get base URL from the current request
            var httpRequest = _httpContextAccessor.HttpContext?.Request;
            if (httpRequest != null)
            {
                string baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
                return $"{baseUrl}{relativePath}";
            }

            // Fallback to relative path if HttpContext is not available
            return relativePath;

        }

        //public async Task<ResponseModel> GetStudentByStudentInfoIdAsync(long studentInfoId, string clientId)
        //{
        //    try
        //    {
        //        // Format the endpoint using your constants
        //        string formattedEndpoint = string.Format(
        //            ProxyConstant.Clientstudentgetbystudentinfoid_getstudentbystudentinfoid, 3, studentInfoId);

        //        // Call your ApiHelper which handles everything (including HttpClient creation)
        //        var response = await ApiHelper.ApiConnection<ResponseModel>(
        //            _httpClientFactory,
        //            student_Universal_API_Host,
        //            formattedEndpoint,
        //            HttpMethod.Get,
        //            null);

        //        if (response.IsSuccess && response.ResponseData != null)
        //        {
        //            // Deserialize ResponseData into LoginResponseDto
        //            var student = JsonConvert.DeserializeObject<StudentDTO>(
        //        response.ResponseData.ToString()
        //            );

        //            // Optional: attach parsed object back to response (for downstream use)
        //            response.ResponseData = student;
        //        }

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentByStudentInfoIdAsync", ex.Message + " | " + ex.StackTrace);
        //        throw new ApplicationException($"Error occurred while fetching students by StudentInfoId: {studentInfoId}.", ex);
        //    }
        //}

        public async Task<ResponseModel?> GetStudentByPhoneAsync(string phoneNo, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetbyphone_getstudentbyphone, 4, phoneNo);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var student = JsonConvert.DeserializeObject<StudentDTO>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = student;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentByPhoneAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by Phone: {phoneNo}.", ex);
            }
        }


        public async Task<ResponseModel> GetStudentsByCurrentSessionAsync(string currentSession, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetbycurrentsession_getstudentsbycurrentsession, 5, currentSession);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetStudentsByCurrentSessionAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by CurrentSession: {currentSession}.", ex);
            }
        }

        public async Task<ResponseModel> GetAllStudentsOnSectionIDAsync(string sectionId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.ClientCresentstudentgetbysection_getstudentsbysection, 7, sectionId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllStudentsOnSectionIDAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by SectionID: {sectionId}.", ex);
            }
        }


        public async Task<ResponseModel> GetOnlyActiveStudentsOnClassIDAsync(long classId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_OnlyActiveStudentsOnClassID, 8, classId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetOnlyActiveStudentsOnClassIDAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by classID: {classId}.", ex);
            }
        }


        public async Task<ResponseModel> GetOnlyActiveStudentsOnSectionIDAsync(long sectionId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_OnlyActiveStudentsOnSectionID, 9, sectionId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetOnlyActiveStudentsOnSectionIDAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by SectionID: {sectionId}.", ex);
            }
        }


        public async Task<ResponseModel> GetMaxRollnoAsync(string sectionId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_MaxRollno, 10, sectionId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetMaxRollnoAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by sectionId: {sectionId}.", ex);
            }
        }

        public async Task<ResponseModel> GetAllStudentsOnClassIDAsync(string classId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_GetAllStudentsOnClassID, 11, classId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var student = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = student;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllStudentsOnClassIDAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by classId: {classId}.", ex);
            }
        }


        public async Task<ResponseModel> GetAllDischargedStudentsOnSectionIDAsync(string sectionId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_GetAllDischargedStudentsOnSectionID, 12, sectionId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var student = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = student;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllDischargedStudentsOnSectionIDAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by sectionId: {sectionId}.", ex);
            }
        }


        public async Task<ResponseModel> TotalStudentsRollForDashBoardAsync(string session, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_TotalStudentsRollForDashBoard, 13, session);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var parts = response.ResponseData.ToString().Split('|');

                    // Updated to 7 parts: TS|TMS|TFS|TotalEmployees|ActiveEmployees|InactiveEmployees|TotalRoutes
                    // Updated to 9 parts: TS|TMS|TFS|TotalEmployees|ActiveEmployees|InactiveEmployees|TME|TFE|TotalRoutes
                    if (parts.Length == 11 &&
                            int.TryParse(parts[0], out int total) &&
                            int.TryParse(parts[1], out int male) &&
                            int.TryParse(parts[2], out int female) &&
                            int.TryParse(parts[3], out int totalEmployees) &&
                            int.TryParse(parts[4], out int activeEmployees) &&
                            int.TryParse(parts[5], out int inactiveEmployees) &&
                            int.TryParse(parts[6], out int maleEmployees) &&
                            int.TryParse(parts[7], out int femaleEmployees) &&
                            int.TryParse(parts[8], out int teachingEmployees) &&
                            int.TryParse(parts[9], out int nonTeachingEmployees) &&
                            int.TryParse(parts[10], out int routes))
                                            {
                                                var parsedResult = new Dictionary<string, object>
                            {
                                { "TotalStudents", total },
                                { "TotalMaleStudents", male },
                                { "TotalFemaleStudents", female },
                                { "TotalEmployees", totalEmployees },
                                { "ActiveEmployees", activeEmployees },
                                { "InactiveEmployees", inactiveEmployees },
                                { "TotalMaleEmployees", maleEmployees },
                                { "TotalFemaleEmployees", femaleEmployees },
                                { "TeachingEmployees", teachingEmployees },
                                { "NonTeachingEmployees", nonTeachingEmployees },
                                { "TotalRoutes", routes }
                            };

                        response.ResponseData = parsedResult;
                    }

                    return response;

                }

                return response;

            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "StudentClient",
                    "TotalStudentsRollForDashBoardAsync",
                    ex.Message + " | " + ex.StackTrace);

                throw new ApplicationException($"Error occurred while fetching students by session: {session}.", ex);
            }
        }

        public async Task<ResponseModel> AttendanceDashboardForDate(string session, string clientId)
        {
            try
            {
                // Format the endpoint using your constants (adjust if needed)
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_AttendanceDashboard, 21, session);

                // Call your ApiHelper to handle HttpClient
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Split pipe-separated string returned by backend (if backend still returns "Present|Absent")
                    var parts = response.ResponseData.ToString().Split('|');

                    if (parts.Length == 4 &&
        int.TryParse(parts[0], out int present) &&
        int.TryParse(parts[1], out int absent) &&
        int.TryParse(parts[2], out int leave) &&
        int.TryParse(parts[3], out int halfDay))
                    {
                        var parsedResult = new Dictionary<string, object>
        {
            { "PresentToday", present },
            { "AbsentToday", absent },
            { "LeaveToday", leave },
            { "HalfDayToday", halfDay }
        };

                        response.ResponseData = parsedResult;
                    }

                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "StudentClient",
                    "AttendanceDashboardForDate",
                    ex.Message + " | " + ex.StackTrace);

                throw new ApplicationException($"Error occurred while fetching attendance for session: {session}.", ex);
            }
        }


        public async Task<ResponseModel> ClassWisStudentsRollForDashBoardAsync(string session, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_ClassWisStudentsRollForDashBoard, 14, session);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var student = JsonConvert.DeserializeObject<List<ClassWiseStudentsRollDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = student;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "ClassWisStudentsRollForDashBoardAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by session: {session}.", ex);
            }
        }

        public async Task<ResponseModel> TotalStudentsRollForDashBoardOnDateAsync(string session, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_TotalStudentsRollForDashBoardOnDate, 15, session);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(
                response.ResponseData.ToString());

                    response.ResponseData = data;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "TotalStudentsRollForDashBoardOnDateAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by session: {session}.", ex);
            }
        }


        public async Task<ResponseModel> SectionWisStudentsRollWithAttendanceForDashBoardAsync(string ClassID, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_SectionWisStudentsRollWithAttendanceForDashBoard, 16, ClassID);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<ClassWiseStudentsRollDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "SectionWisStudentsRollWithAttendanceForDashBoardAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by ClassID: {ClassID}.", ex);
            }
        }


        public async Task<ResponseModel> GetAllStudentsOnStudentNameAndCSessionAsync(string NameAndsession, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_GetAllStudentsOnStudentNameAndCSession, 17, NameAndsession);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllStudentsOnStudentNameAndCSessionAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by NameAndsession: {NameAndsession}.", ex);
            }
        }


        public async Task<ResponseModel> GetBoardNoWithDateAsync(string classSectionId, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_GetBoardNoWithDate, 18, classSectionId);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetBoardNoWithDateAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by classSectionId: {classSectionId}.", ex);
            }
        }

        public async Task<ResponseModel> GetAllStudentsOnSessionAsync(string session, string clientId)
        {
            try
            {
                // Format the endpoint using your constants
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_GetAllStudentsOnSession, 19, session);

                // Call your ApiHelper which handles everything (including HttpClient creation)
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var studentlist = JsonConvert.DeserializeObject<List<StudentDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = studentlist;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllStudentsOnSessionAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching students by session: {session}.", ex);
            }
        }

        public async Task<ResponseModel> GetNextAdmissionNoAsync(string clientId)
        {
            try
            {
                // Use constant for endpoint URL (GET method, no parameters)
                string formattedEndpoint = string.Format(ProxyConstant.Clientstudentgetby_GetNextAdmissionNo, 6);

                // Call API via helper
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null);

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetNextAdmissionNoAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while retrieving the next admission number.", ex);
            }
        }


        // Update student
        public async Task<ResponseModel> UpdateStudentAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Format endpoint using constants (assuming client ID is inserted)
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentpost_UpdateStudent, 0); // Or use clientId directly if dynamic

                // Call API using ApiHelper
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    request);

                // Optional: Deserialize ResponseData if it's expected to be a typed object
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var updatedStudent = JsonConvert.DeserializeObject<StudentDTO>(
                        response.ResponseData.ToString());

                    response.ResponseData = updatedStudent;
                }

                return response;
            }
            catch (Exception ex)
            {
                // Log error
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateStudentAsync", ex.Message + " | " + ex.StackTrace);

                // Throw wrapped exception
                throw new ApplicationException("Error occurred while updating student.", ex);
            }
        }
        public async Task<ResponseModel> UpdateParentDetailAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // 1️⃣ Get StudentID from microservice
                var studentResponse = await GetStudentId(request.StudentInfoID, clientId);

                if (!studentResponse.IsSuccess || studentResponse.ResponseData == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Status = 0,
                        Message = studentResponse.Message ?? "Invalid StudentInfoID!"
                    };
                }

                if (studentResponse.ResponseData is StudentIdDto dto &&
                      !string.IsNullOrWhiteSpace(dto.StudentID))
                {
                    string studentId = dto.StudentID;

                    // 2️⃣ Save Father Photo (if exists)
                  

                    // 3️⃣ Save Mother Photo (if exists)
                    if (request.MotherPhoto != null && request.MotherPhoto.Length > 0)
                    {
                        string motherPhotoUrl = await SavePhotoAsync(request.MotherPhoto, clientId, studentId, "MotherPhoto");
                        request.MotherPhotoPath = motherPhotoUrl;
                        request.MotherPhoto = null; // remove file from request
                    }
                }
                else
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Status = 0,
                        Message = "Invalid Student ID received from microservice."
                    };
                }

                // 4️⃣ Call microservice via proxy
                string formattedEndpoint = string.Format(ProxyConstant.Clientstudentput_UpdateParentDetail, 1); // actionType = 1
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request // send request with URLs
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "StudentClient",
                    "UpdateParentDetailAsync",
                    ex.Message + " | " + ex.StackTrace
                );

                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        // Helper method for saving parent photos
        private async Task<string?> SavePhotoAsync(IFormFile photo, string clientId, string studentId, string folder)
        {
            if (photo == null || photo.Length == 0)
                return null;

            try
            {
                // Build folder path dynamically
                string photoRoot = Path.Combine(_env.ContentRootPath, "ClientData", clientId, studentId.ToString(), folder);
                if (!Directory.Exists(photoRoot))
                    Directory.CreateDirectory(photoRoot);

                // ✅ Remove any existing files (keep only one image per student)
                foreach (var existingFile in Directory.GetFiles(photoRoot))
                {
                    File.Delete(existingFile);
                }

                // ✅ Use a consistent file name (like StudentID + extension)
                string fileName = $"{studentId}{Path.GetExtension(photo.FileName)}";
                string fullPath = Path.Combine(photoRoot, fileName);

                // Save file
                await using var stream = new FileStream(fullPath, FileMode.Create);
                await photo.CopyToAsync(stream);

                // Build accessible URL
                var httpRequest = _httpContextAccessor.HttpContext?.Request;
                string baseUrl = httpRequest != null ? $"{httpRequest.Scheme}://{httpRequest.Host}" : "";
                return $"{baseUrl}/ClientData/{clientId}/{studentId}/{folder}/{fileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving photo: {ex.Message}");
                return null;
            }
        }



        //public async Task<ResponseModel> UpdateParentDetailAsync(UpdateStudentRequestDTO request, string clientId)
        //{
        //    try
        //    {
        //        // Format the endpoint using a constant
        //        string formattedEndpoint = string.Format(
        //            ProxyConstant.Clientstudentput_UpdateParentDetail, 1); // Assuming actionType = 1 for parent detail update

        //        // Make proxy API call using helper
        //        var response = await ApiHelper.ApiConnection<ResponseModel>(
        //            _httpClientFactory,
        //            student_Universal_API_Host,
        //            formattedEndpoint,
        //            HttpMethod.Put,
        //            request // request object as body
        //        );

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateParentDetailAsync", ex.Message + " | " + ex.StackTrace);
        //        throw new ApplicationException("Error occurred while updating parent details.", ex);
        //    }
        //}

        public async Task<ResponseModel> UpdateAddressDetailAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Format the proxy endpoint; assuming actionType = 2 is used for address detail updates
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentput_UpdateAddressDetail, 2);

                // Perform the proxy API call via the helper
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request // send DTO as body
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateAddressDetailAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating address details.", ex);
            }
        }
        public async Task<ResponseModel> UpdatePersonalDetailAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Handle photo upload in gateway
                if (request.StudentPhoto != null && request.StudentPhoto.Length > 0)
                {
                    // Get student ID from microservice
                    var studentResponse = await GetStudentId(request.StudentInfoID, clientId);

                    if (!studentResponse.IsSuccess || studentResponse.ResponseData == null)
                    {
                        return new ResponseModel
                        {
                            IsSuccess = false,
                            Status = 0,
                            Message = studentResponse.Message ?? "Invalid StudentInfoID!"
                        };
                    }

                    // ✅ Cast ResponseData to StudentIdDto instead of ToString()
                    if (studentResponse.ResponseData is StudentIdDto dto &&
                        !string.IsNullOrWhiteSpace(dto.StudentID))
                    {
                        string studentId = dto.StudentID;
                        // Save photo
                        string photoPath = await SavePhotoAsync(request.StudentPhoto, clientId, studentId, "StudentPhoto");

                        // Update the request with the photo path
                        request.PhotoPath = photoPath;

                        // Remove the file from request to avoid serialization issues
                        request.StudentPhoto = null;
                    }
                    else
                    {
                        return new ResponseModel
                        {
                            IsSuccess = false,
                            Status = 0,
                            Message = "Invalid Student ID received from microservice."
                        };
                    }
                }

                // Format the proxy endpoint; assuming actionType = 3 corresponds to "Personal Detail Update"
                string formattedEndpoint = string.Format(ProxyConstant.Clientstudentput_UpdatePersonalDetail, 3);

                // Send the updated request via proxy to the microservice
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request // Request body
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "StudentClient",
                    "UpdatePersonalDetailAsync",
                    ex.Message + " | " + ex.StackTrace
                );

                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
        private async Task<ResponseModel> GetStudentId(string studentInfoId, string clientId)
        {
            var response = new ResponseModel
            {
                IsSuccess = false,
                Message = "Student ID not found",
                Status = 0,
                ResponseData = null
            };

            try
            {
                if (string.IsNullOrWhiteSpace(studentInfoId))
                {
                    response.Message = "StudentInfoID is required";
                    return response;
                }

                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentget_GetStudentId,
                    20,
                    studentInfoId
                );

                // 🔹 Call API and get ResponseModel (not generic)
                var apiResponse = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get
                );

                if (apiResponse != null && apiResponse.IsSuccess && apiResponse.ResponseData != null)
                {
                    try
                    {
                        // Deserialize ResponseData into StudentIdDto
                        var dto = JsonConvert.DeserializeObject<StudentIdDto>(
                            apiResponse.ResponseData.ToString() ?? string.Empty
                        );

                        if (dto != null)
                        {
                            response.IsSuccess = true;
                            response.Status = 1;
                            response.Message = "Student ID retrieved successfully";
                            response.ResponseData = dto;
                        }
                        else
                        {
                            response.Message = "Unable to parse StudentIdDto";
                        }
                    }
                    catch
                    {
                        // fallback: maybe it's a plain number
                        if (int.TryParse(apiResponse.ResponseData.ToString(), out int studentId))
                        {
                            response.IsSuccess = true;
                            response.Status = 1;
                            response.Message = "Student ID retrieved successfully";
                            response.ResponseData = new StudentIdDto { StudentID = studentId.ToString() };
                        }
                        else
                        {
                            response.Message = "Invalid response format";
                        }
                    }
                }
                else
                {
                    response.Message = apiResponse?.Message ?? "No student found";
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "StudentClient",
                    "GetStudentIdFromMicroservice",
                    ex.Message + " | " + ex.StackTrace
                );

                response.Message = "Error retrieving Student ID";
                response.Error = ex.Message;
                return response;
            }
        }
        public async Task<ResponseModel> UpdateStudentRollNoAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Format the proxy endpoint. Assume actionType = 4 is for Roll No update.
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentpost_PostUpdateStudentRollNo, 4);

                // Send the request using the proxy API helper
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateStudentRollNoAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating student roll number.", ex);
            }
        }
        public async Task<ResponseModel> UpdateClassStudentRollNumbers(List<StudentRollNoUpdate> updates, string clientId)
        {
            var response = new ResponseModel
            {
                IsSuccess = false,
                Status = 400,
                Message = "Update request failed"
            };

            try
            {
                // 1. Validate input
                if (string.IsNullOrWhiteSpace(clientId))
                {
                    response.Message = "Client ID is required";
                    return response;
                }

                if (updates == null || !updates.Any())
                {
                    response.Message = "No student updates provided";
                    return response;
                }

                // 2. Filter valid updates
                var validUpdates = updates
                    .Where(u => u != null && u.StudentInfoID > 0 && !string.IsNullOrWhiteSpace(u.RollNo))
                    .ToList();

                if (!validUpdates.Any())
                {
                    response.Message = "No valid student updates provided";
                    return response;
                }

                // 3. Prepare API request
                string formattedEndpoint = string.Format(
                    ProxyConstant.ClientstudentUpdate_UpdateClassStudentRollNumbers,
                    0); // Assuming 16 is your actionType for roll number updates

                // 4. Add client ID to headers or request
                var requestBody = new
                {
                    BulkUpdates = updates // Match the microservice's expected format
                };

                var apiResponse = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    requestBody
                );

                // 6. Handle API response
                if (apiResponse == null)
                {
                    response.Message = "No response received from API";
                    return response;
                }

                return apiResponse;
            }
            catch (HttpRequestException httpEx)
            {
                response.Status = 502; // Bad Gateway
                response.Message = $"API connection failed: {httpEx.Message}";
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateClassStudentRollNumbers",
                    $"HTTP Error: {httpEx.StatusCode} - {httpEx.Message}");
            }

            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Unexpected error during update";
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateClassStudentRollNumbers",
                    $"Exception: {ex.Message} | {ex.StackTrace}");
            }

            return response;
        }
        public async Task<ResponseModel> UpdateBoardNoAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Format the proxy endpoint — assuming actionType = 5 for board number update
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentput_UpdateStudentBoardNo, 5);

                // Call the proxy API helper method
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateBoardNoAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating student board number.", ex);
            }
        }

        public async Task<ResponseModel> UpdateDOBAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Format the proxy endpoint — assuming actionType = 6 for DOB update
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentput_UpdateStudentDOB, 6);

                // Make the proxy API call
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateDOBAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating student DOB.", ex);
            }
        }

        public async Task<ResponseModel> UpdateSectionAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Format the proxy endpoint — assuming actionType = 7 for updating section
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentpost_PostUpdateStudentSection, 7);

                // Make the proxy API call using the helper
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateSectionAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating student section.", ex);
            }
        }


        public async Task<ResponseModel> UpdateClassAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Assuming actionType = 8 for updating class (confirm this with backend)
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentput_PutUpdateStudentClass, 8);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateClassAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating student class.", ex);
            }
        }

        public async Task<ResponseModel> DischargeStudentAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Assuming actionType = 9 is used for discharging student
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentpost_PostDischargeStudent, 9);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "DischargeStudentAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while discharging the student.", ex);
            }
        }


        public async Task<ResponseModel> DischargeStudentForIntValueAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Assuming actionType = 10 is used for "DischargeStudentForIntValue"
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentpost_PostDischargeStudentForIntValue, 10);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "DischargeStudentForIntValueAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while discharging the student for int value.", ex);
            }
        }


        public async Task<ResponseModel> RejoinStudentAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Assuming actionType = 11 is used for Rejoin Student
                string formattedEndpoint = string.Format(
                    ProxyConstant.ClientstudentPut_PutRejoinStudent, 11);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "RejoinStudentAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while rejoining the student.", ex);
            }
        }

        public async Task<ResponseModel> RejoinStudentForIntValueAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Assuming actionType = 12 corresponds to RejoinStudentForIntValue
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentpost_PostRejoinStudentForIntValue, 12);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "RejoinStudentForIntValueAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while rejoining student (int value).", ex);
            }
        }


        public async Task<ResponseModel> UpdateStudentEducationAdmissionPrePrimaryEtcAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Assuming actionType = 13 is mapped to UpdateStudentEducationAdmissionPrePrimaryEtc in your microservice
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentpost_PostUpdateStudentEducationAdmissionPrePrimaryEtc, 13);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateStudentEducationAdmissionPrePrimaryEtcAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating pre-primary or education admission details.", ex);
            }
        }


        public async Task<ResponseModel> UpdateStudentHeightWeightAdharNamePENEtcUDISEAsync(UpdateStudentRequestDTO request, string clientId)
        {
            try
            {
                // Assuming actionType = 14 is mapped to UpdateStudentHeightWeightAdharNamePENEtcUDISE in your microservice
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentpost_PostUpdateStudentHeightWeightAdharNamePENEtcUDISE, 14);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateStudentHeightWeightAdharNamePENEtcUDISEAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating height, weight, Aadhaar, PEN, or UDISE details.", ex);
            }
        }

        public async Task<ResponseModel> UpdateStudentSessionAsync(StudentSessionUpdateRequest request, string clientId)
        {
            try
            {
                // Assuming actionType = 15 is mapped to UpdateStudentSession in your microservice (adjust the actionType accordingly)
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentput_UpdateStudentSession, 15);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    request
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "UpdateStudentSessionAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating student session details.", ex);
            }
        }
        public async Task<ResponseModel> GetAllSessions(string clientId)
        {
            try
            {
                // Use the correct endpoint constant (no need to format if static)
                string formattedEndpoint = string.Format(
                    ProxyConstant.Clientstudentgetby_GetAllSessions, 19);

                // Call the backend service using ApiHelper
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                // If response is successful and ResponseData is not null
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into a list of SessionDto
                    var sessionList = JsonConvert.DeserializeObject<List<SessionDto>>(
                        response.ResponseData.ToString()
                    );

                    // Optionally assign it back (so controller or consumer can use the typed list)
                    response.ResponseData = sessionList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentClient", "GetAllSessions", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while retrieving sessions.", ex);
            }
        }


    }

}
