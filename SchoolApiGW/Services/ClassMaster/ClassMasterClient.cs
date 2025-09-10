using Microsoft.Extensions.Configuration;
using SchoolApiGW.Helper;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using SchoolApiGW.Services.Transport;

namespace SchoolApiGW.Services.ClassMaster
{
    public class ClassMasterClient : ProxyBaseUrl, IClassMasterClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ClassMasterClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> GetEducationalDepartments(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassMaster_GetEducationalDepartments;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var educationalList = JsonConvert.DeserializeObject<List<EducationalDepartmentDto>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = educationalList;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "GetEducationalDepartments", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting educational departments.", ex);
            }
        }

        public async Task<ResponseModel> GetSectionsByClassId(int classId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassMaster_GetSectionsByClassId, classId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var sectionList = JsonConvert.DeserializeObject<List<SectionDto>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = sectionList;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "GetSectionsByClassId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting sections by class id.", ex);
            }
        }

        public async Task<ResponseModel> GetClassesBySessionWithDepartment(string session, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassMaster_GetClassesBySessionWithDepartment, session);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var classList = JsonConvert.DeserializeObject<List<ClassDto>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = classList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "GetClassesBySessionWithDepartment", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting classes by session with department.", ex);
            }
        }

        public async Task<ResponseModel> AddClass(ClassDto classDto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassMaster_AddClass;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    classDto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "AddClass", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding class.", ex);
            }
        }

        public async Task<ResponseModel> AddSection(SectionDto sectionDto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassMaster_AddSection;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    sectionDto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "AddSection", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding section.", ex);
            }
        }
        public async Task<ResponseModel> UpgradeClassSubjectsSectionsAsync(UpgradeClassDto upgradeDto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassMaster_UpgradeClassesSubjectsSections;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    upgradeDto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "UpgradeClassesSubjectsSectionsAsync", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while upgrading classes, subjects, and sections.", ex);
            }
        }

        public async Task<ResponseModel> UpdateClass(ClassDto classDto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassMaster_UpdateClass;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    classDto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "UpdateClass", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating class.", ex);
            }
        }


      
        public async Task<ResponseModel> UpdateSection(SectionDto sectionDto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.ClassMaster_UpdateSection;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    sectionDto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "UpdateSection", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating section.", ex);
            }
        }

        public async Task<ResponseModel> DeleteClass(int classId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassMaster_DeleteClass, classId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "DeleteClass", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting class.", ex);
            }
        }

        public async Task<ResponseModel> DeleteSection(int sectionId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.ClassMaster_DeleteSection, sectionId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterClient", "DeleteSection", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting section.", ex);
            }
        }

        public Task<ResponseModel> UpgradeClassSubjectsSectionsAsync(string currentSession, string newSession, string clientId)
        {
            throw new NotImplementedException();
        }
    }
}
