using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.Employee;

namespace SchoolApiGW.Services.Qualifications
{
    public class QualificationsClient : ProxyBaseUrl, IQualificationsClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public QualificationsClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> GetQualifications(string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetQualifications);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var qualificationList = JsonConvert.DeserializeObject<List<QualificationModel>>(response.ResponseData.ToString());
                    response.ResponseData = qualificationList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetQualifications", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching qualifications.", ex);
            }
        }

        public async Task<ResponseModel> GetQualificationById(string qualificationId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetQualificationById, qualificationId);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var qualification = JsonConvert.DeserializeObject<QualificationModel>(response.ResponseData.ToString());
                    response.ResponseData = qualification;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetQualificationById", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching qualification by ID: {qualificationId}.", ex);
            }
        }

        public async Task<ResponseModel> AddQualification(QualificationModel qualification, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_AddQualification);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    qualification
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("QualificationClient", "AddQualification", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding qualification.", ex);
            }
        }


        public async Task<ResponseModel> UpdateQualification(QualificationModel qualification, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_UpdateQualification);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    qualification
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("QualificationClient", "UpdateQualification", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating qualification.", ex);
            }
        }


        public async Task<ResponseModel> DeleteQualification(string qualificationId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_DeleteQualification, qualificationId);


                var requestPayload = new
                {
                    QualificationID = qualificationId
                };

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Delete,
                    requestPayload
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("QualificationClient", "DeleteQualification", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while deleting qualification ID: {qualificationId}.", ex);
            }
        }

    }
}
