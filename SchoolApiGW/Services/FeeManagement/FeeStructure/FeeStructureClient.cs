using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SchoolApiGW.Helper;
using System.Net.Http;

namespace SchoolApiGW.Services.FeeManagement.FeeStructure
{
    public class FeeStructureClient : ProxyBaseUrl, IFeeStructureClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FeeStructureClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddFeeStructure(FeeStructureDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.FeeStructure_AddFeeStructure;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    dto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureClient", "AddFeeStructure", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding fee structure.", ex);
            }
        }

        public async Task<ResponseModel> UpdateFeeStructure(FeeStructureDto dto, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.FeeStructure_UpdateFeeStructure;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    dto
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureClient", "UpdateFeeStructure", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating fee structure.", ex);
            }
        }

        public async Task<ResponseModel> DeleteFeeStructure(long fsId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeStructure_DeleteFeeStructure, fsId);
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
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureClient", "DeleteFeeStructure", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting fee structure.", ex);
            }
        }

        public async Task<ResponseModel> GetFeeStructureById(long fsId, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeStructure_GetFeeStructureById, fsId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var dto = JsonConvert.DeserializeObject<FeeStructureDto>(response.ResponseData.ToString());
                    response.ResponseData = dto;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureClient", "GetFeeStructureById", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching fee structure by ID: {fsId}.", ex);
            }
        }

        public async Task<ResponseModel> GetAllFeeStructures(string clientId, string currentSession)
        {
            try
            {
                // Add currentSession as query parameter
                string endpoint = string.Format(ProxyConstant.FeeStructure_GetAllFeeStructures, currentSession);
               // string endpoint = $"{ProxyConstant.FeeStructure_GetAllFeeStructures}?currentSession={currentSession}";

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<FeeStructureDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "FeeStructureClient",
                    "GetAllFeeStructures",
                    ex.Message + " | " + ex.StackTrace
                );
                throw new ApplicationException("Error occurred while fetching all fee structures.", ex);
            }
        }


        public async Task<ResponseModel> GetFeeStructuresByClassId(long cIDFK, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.FeeStructure_GetFeeStructuresByClassId, cIDFK);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<FeeStructureDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("FeeStructureClient", "GetFeeStructuresByClassId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException($"Error occurred while fetching fee structures for class ID: {cIDFK}.", ex);
            }
        }

        public async Task<ResponseModel> GetFeeStructureByClassAndFeeHead(long classId, long fhId, string clientId)
        {
            try
            {
                // Build the endpoint URL using the ProxyConstant
                string endpoint = string.Format(ProxyConstant.FeeStructure_GetByClassAndFeeHead, classId, fhId);

                // Call the microservice API
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    FeeManagement_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                // Deserialize the response data into the DTO list
                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<FeeStructureDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog(
                    "FeeStructureClient",
                    "GetFeeStructureByClassAndFeeHead",
                    ex.Message + " | " + ex.StackTrace
                );
                throw new ApplicationException($"Error occurred while fetching fee structure for ClassID: {classId} and FHID: {fhId}.", ex);
            }
        }

    }
}
