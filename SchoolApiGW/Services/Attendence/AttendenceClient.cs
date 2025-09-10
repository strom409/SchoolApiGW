using Microsoft.Extensions.Configuration;
using SchoolApiGW.Helper;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using SchoolApiGW.Services.Transport;

namespace SchoolApiGW.Services.Attendence
{
    public class AttendenceClient : ProxyBaseUrl, IAttendenceClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public AttendenceClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddAttendance(AttendanceDTO attendance, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Attendance_AddAttendance;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    attendance
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "AddAttendance", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding attendance.", ex);
            }
        }

        public async Task<ResponseModel> AddAttendanceList(List<AttendanceDTO> attendanceList, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Attendance_AddAttendanceList;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    attendanceList
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "AddAttendanceList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding attendance list.", ex);
            }
        }

        public async Task<ResponseModel> UpdateTodaysAttendance(AttendanceDTO attendance, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Attendance_UpdateTodaysAttendance;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    attendance
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "UpdateTodaysAttendance", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating today's attendance.", ex);
            }
        }

        public async Task<ResponseModel> GetTodaysAttendance(string sessionId, string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_GetTodaysAttendance, sessionId, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var attendanceList = JsonConvert.DeserializeObject<List<AttendanceDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = attendanceList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "GetTodaysAttendance", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting today's attendance.", ex);
            }
        }

        public async Task<ResponseModel> GetEditAttendance(string sectionId, string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_GetEditAttendance, sectionId, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var attendanceList = JsonConvert.DeserializeObject<List<AttendanceDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = attendanceList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "GetEditAttendance", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting edit attendance.", ex);
            }
        }

        public async Task<ResponseModel> GetAbsentList(string sectionId, string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_GetAbsentList, sectionId, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var attendanceList = JsonConvert.DeserializeObject<List<AttendanceDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = attendanceList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "GetAbsentList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting absent list.", ex);
            }
        }

        public async Task<ResponseModel> getAttendanceListOnDates(string dateFrom, string dateTo, string Session, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_GetAttendanceListOnDates, dateFrom, dateTo, Session);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var attendanceList = JsonConvert.DeserializeObject<List<AttendanceDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = attendanceList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "getAttendanceListOnDates", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting attendance list on dates.", ex);
            }
        }

        public async Task<ResponseModel> CheckAttendanceAddedorNot(string classId, string sectionId, string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_CheckAttendanceAddedorNot, classId, sectionId, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );


                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "CheckAttendanceAddedorNot", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while checking if attendance is added or not.", ex);
            }
        }

        public async Task<ResponseModel> GetMonthlyAttendance(string session, string dateFrom, string dateTo, string className, string sectionName, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_GetMonthlyAttendance,
                                        session,
                                        dateFrom,
                                        dateTo,
                                        className,
                                        sectionName);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize as dynamic attendance object (List<Dictionary<string, object>>)
                    var attendanceList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(
                        response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = attendanceList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "GetMonthlyAttendance", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting monthly attendance.", ex);
            }
        }

        public async Task<ResponseModel> getAttendanceListOnDateswithclassid(string dateFrom, string dateTo, string classId, string sectionId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_GetAttendanceListOnDatesWithClassId, dateFrom, dateTo, classId,sectionId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var attendanceList = JsonConvert.DeserializeObject<List<AttendanceDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = attendanceList;
                }


                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "getAttendanceListOnDateswithclassid", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting attendance list on dates with class id.", ex);
            }
        }

        public async Task<ResponseModel> GetPendingAttendanceStudents(string classId, string sectionId, string session, string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_GetPendingAttendanceStudents, classId, sectionId, session, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "GetPendingAttendanceStudents", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting pending attendance students.", ex);
            }
        }

        public async Task<ResponseModel> GetAttendanceReport(string classId, string sectionId, string dateFrom, string dateTo, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Attendance_GetAttendanceReport, classId, sectionId, dateFrom, dateTo);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Attendence_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize to a dynamic List of Dictionary (or define a DTO if you prefer)
                    var reportData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(
                        response.ResponseData.ToString()
                    );

                    response.ResponseData = reportData;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("AttendenceClient", "GetAttendanceReport", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting attendance report.", ex);
            }
        }

    }
}