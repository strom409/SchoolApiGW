using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.FeeManagement.FeeHead;

namespace SchoolApiGW.Services.FeeManagement.FeeDueRebate
{
    public class FeeDueRebateClient : ProxyBaseUrl, IFeeDueRebateClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FeeDueRebateClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseModel> AddFeeDueRebate(FeeDueRebateDTO request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.FeeDueRebate_Add;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    request
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueRebateClient", "AddFeeDueRebate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding FeeDueRebate.", ex);
            }
        }
        public async Task<ResponseModel> UpdateFeeDueRebate(FeeDueRebateDTO request, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.FeeDueRebate_Update;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    request
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueRebateClient", "UpdateFeeDueRebate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating FeeDueRebate.", ex);
            }
        }

        public async Task<ResponseModel> DeleteFeeDueRebate(long rebateId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeDueRebate_Delete, rebateId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Delete,
                    null
                );

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueRebateClient", "DeleteFeeDueRebate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting FeeDueRebate.", ex);
            }
        }

        public async Task<ResponseModel> GetFeeDueRebateByStudentName(string studentName, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeDueRebate_GetFeeDueByStudentName, studentName);
             
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueRebateClient", "GetFeeDueRebateByStudentName", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching FeeDueRebate by StudentName.", ex);
            }
        }
        public async Task<ResponseModel> GetFeeDueRebateByAdmissionNo(string param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeDueRebate_GetFeeDueRebateByAdmissionNo, param);
              
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueRebateClient", "GetFeeDueRebateByAdmissionNo", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching FeeDueRebate by AdmissionNo.", ex);
            }
        }
        public async Task<ResponseModel> GetFeeDueRebateByClassId(long classId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeDueRebate_GetByClassId, classId);
               
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeDueRebateClient", "GetFeeDueRebateByClassId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching FeeDueRebate by ClassId.", ex);
            }
        }
    }
}

