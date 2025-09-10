using SchoolApiGW.Helper;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SchoolApiGW.Services.TeacherLog
{
    public class TeacherLogClient : ProxyBaseUrl, ITeacherLogClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public TeacherLogClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddTeacherLogDataOnSectionIDandDate(TeacherLogData td, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.TeacherLog_AddTeacherLogDataOnSectionIDandDate;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    td
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "AddTeacherLogDataOnSectionIDandDate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding teacher log data.", ex);
            }
        }

        public async Task<ResponseModel> AddTeacherLogForNewTiny(TeacherLogData td, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.TeacherLog_AddTeacherLogForNewTiny;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    td
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "AddTeacherLogForNewTiny", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding teacher log for new tiny.", ex);
            }
        }

        public async Task<ResponseModel> AddTeacherPerformance(TeacherLogData td, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.TeacherLog_AddTeacherPerformance;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    td
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "AddTeacherPerformance", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding teacher performance.", ex);
            }
        }

        public async Task<ResponseModel> UpdateTeacherLog(TeacherLogData trd, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.TeacherLog_UpdateTeacherLog;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    trd
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "UpdateTeacherLog", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating teacher log.", ex);
            }
        }

        public async Task<ResponseModel> GetTeacherLogDataOnSectionIDandDate(string sectionIdAndDate, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetTeacherLogDataOnSectionIDandDate, sectionIdAndDate);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeacherLogDataOnSectionIDandDate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teacher log data by section ID and date.", ex);
            }
        }

        public async Task<ResponseModel> GetTeacherLogDataOnECodeandDate(string eCodeAndDate, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetTeacherLogDataOnECodeandDate, eCodeAndDate);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeacherLogDataOnECodeandDate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teacher log data by employee code and date.", ex);
            }
        }

        public async Task<ResponseModel> GetSubjectList(string classId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetSubjectList, classId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<object>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetSubjectList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching subject list.", ex);
            }
        }

        public async Task<ResponseModel> GetSubSubjectList(string subjectId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetSubSubjectList, subjectId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<object>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetSubSubjectList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching sub subject list.", ex);
            }
        }

        public async Task<ResponseModel> GetOptSubjectList(string classId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetOptSubjectList, classId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<object>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetOptSubjectList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching optional subject list.", ex);
            }
        }

        public async Task<ResponseModel> GetTeacherLogOnDateAndCodeList(string empcode, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetTeacherLogOnDateAndCodeList, empcode);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeacherLogOnDateAndCodeList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teacher log on date and code list.", ex);
            }
        }

        public async Task<ResponseModel> GetTeacherLogOnDateList(string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetTeacherLogOnDateList, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeacherLogOnDateList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teacher log on date list.", ex);
            }
        }

        public async Task<ResponseModel> GetTeacherPerformance(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.TeacherLog_GetTeacherPerformance;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeacherPerformance", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teacher performance.", ex);
            }
        }

        public async Task<ResponseModel> GetTeachersLog(string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetTeachersLog, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeachersLog", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teachers log.", ex);
            }
        }

        public async Task<ResponseModel> GetTeachersLogfromTT(string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetTeachersLogfromTT, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeachersLogfromTT", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teachers log from TT.", ex);
            }
        }

        public async Task<ResponseModel> GetTeachersLogFromTTEmpty(string date, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetTeachersLogFromTTEmpty, date);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeachersLogFromTTEmpty", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teachers log from TT empty.", ex);
            }
        }

        public async Task<ResponseModel> GetTeachersLogRangeWise(string dateRange, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.TeacherLog_GetTeachersLogRangeWise, dateRange);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<TeacherLogData>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "GetTeachersLogRangeWise", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching teachers log range wise.", ex);
            }
        }

        public async Task<ResponseModel> DeleteTeacherLogOnLogID(TeacherLogData td, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.TeacherLog_DeleteTeacherLogOnLogID;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    teacherlog_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    td
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TeacherLogClient", "DeleteTeacherLogOnLogID", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting teacher log on log ID.", ex);
            }
        }
    }
}
