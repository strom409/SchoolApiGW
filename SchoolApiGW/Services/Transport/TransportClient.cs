using Microsoft.Extensions.Configuration;
using SchoolApiGW.Helper;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using SchoolApiGW.Services.Students;

namespace SchoolApiGW.Services.Transport
{
    public class TransportClient : ProxyBaseUrl, ITransportClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public TransportClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddTransport(TransportDTO transport, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportAdd_PostAddTransport;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    transport
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "AddTransport", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding a new transport.", ex);
            }
        }

        public async Task<ResponseModel> AddBusStops(TransportDTO transport, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportAddBusStops_PostAddBusStops;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Post,
                    transport
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "AddBusStops", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding bus stops.", ex);
            }
        }

        public async Task<ResponseModel> UpdateTransport(TransportDTO transport, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportUpdate_PostUpdateTransport;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    transport
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "UpdateTransport", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating transport.", ex);
            }
        }

        public async Task<ResponseModel> UpdateBusStops(TransportDTO transport, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportUpdateBusStops_PostUpdateBusStops;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    transport
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "UpdateBusStops", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating bus stops.", ex);
            }
        }

        public async Task<ResponseModel> UpdateBusStopsLatLong(TransportDTO transport, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportUpdateBusStopsLatLong_PostUpdateBusStopsLatLong;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    transport
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "UpdateBusStopsLatLong", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating bus stops lat/long.", ex);
            }
        }

        public async Task<ResponseModel> UpdateBusStopRates(StudentBusReportDTO sbr, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportUpdateBusStopRates_PostUpdateBusStopRates;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    sbr
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "UpdateBusStopRates", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating bus stop rates.", ex);
            }
        }

        public async Task<ResponseModel> updateroute(TransportDTO transport, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportUpdateRoute_PostUpdateRoute;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    transport
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "updateroute", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating route.", ex);
            }
        }

        public async Task<ResponseModel> UpdateStudentRouteAndBusStop(StudentBusReportDTO sbr, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportUpdateStudentRouteAndBusStop_PostUpdateStudentRouteAndBusStop;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    sbr
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "UpdateStudentRouteAndBusStop", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating student route and bus stop.", ex);
            }
        }

        public async Task<ResponseModel> aupdateBusStop(BusStop bs, string clientId)
        {
            try
            {
                string formattedEndpoint = ProxyConstant.TransportAUpdateBusStop_PostAUpdateBusStop;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Put,
                    bs
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "aupdateBusStop", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating a bus stop.", ex);
            }
        }

        public async Task<ResponseModel> GetTransportListOnSession(string session, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetListOnSession_GetTransportListOnSession,0, session);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportList = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetTransportListOnSession", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting transport list on session.", ex);
            }
        }

        public async Task<ResponseModel> GetTransportList(string routeId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetList_GetTransportList, 1,routeId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportList = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportList;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetTransportList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting transport list.", ex);
            }
        }

        public async Task<ResponseModel> GetTransportListRateFromInfo(string routeId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetListRateFromInfo_GetTransportListRateFromInfo,2, routeId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportList = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportList;
                }
                return response;
            
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetTransportListRateFromInfo", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting transport list rate from info.", ex);
            }
        }

        public async Task<ResponseModel> GetTransportListWithBusRate(string routeId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetListWithBusRate_GetTransportListWithBusRate,3, routeId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportList = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportList;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetTransportListWithBusRate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting transport list with bus rate.", ex);
            }
        }

        public async Task<ResponseModel> GetTransportByRouteId(string routeId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetByRouteId_GetTransportByRouteId,4 ,routeId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                ); 

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transport = JsonConvert.DeserializeObject<TransportDTO>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transport;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetTransportByRouteId", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting transport by route id.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentRouteDetails(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetStudentRouteDetails_GetStudentRouteDetails,5, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetStudentRouteDetails", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting student route details.", ex);
            }
        }

        public async Task<ResponseModel> GetStopListByName(string stopName, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetStopListByName_GetStopListByName,6, stopName);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetStopListByName", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting stop list by name.", ex);
            }
        }

        public async Task<ResponseModel> GetAllStops(string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format( ProxyConstant.TransportGetAllStops_GetAllStops,7);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }

            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetAllStops", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting all stops.", ex);
            }
        }

        public async Task<ResponseModel> GetClassIdsAssigned(string userId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetClassIdsAssigned_GetClassIdsAssigned,8, userId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetClassIdsAssigned", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting class ids assigned.", ex);
            }
        }

        public async Task<ResponseModel> GetAssignedSections(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetAssignedSections_GetAssignedSections,9, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetAssignedSections", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting assigned sections.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentBusReportListOnSectionID(string sectionId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetStudentBusReportListOnSectionID_GetStudentBusReportListOnSectionID,10, sectionId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<StudentBusReportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetStudentBusReportListOnSectionID", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting student bus report list on section ID.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentListOnRouteID(string routeId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetStudentListOnRouteID_GetStudentListOnRouteID,11, routeId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<StudentBusReportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetStudentListOnRouteID", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting student list on route ID.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentBusRateClasswise(string classId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetStudentBusRateClasswise_GetStudentBusRateClasswise,12, classId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetStudentBusRateClasswise", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting student bus rate classwise.", ex);
            }
        }

        public async Task<ResponseModel> GetStudentBusRate(string param, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetStudentBusRate_GetStudentBusRate,13, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetStudentBusRate", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting student bus rate.", ex);
            }
        }

        public async Task<ResponseModel> GetTransportNameById(string routeId, string clientId)
        {
            var responseModel = new ResponseModel();

            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetTransportNameById_GetTransportNameById, 14, routeId);

                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response != null && response.IsSuccess && response.ResponseData != null)
                {
                    var routeName = response.ResponseData.ToString();
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Transport name fetched successfully";
                    responseModel.ResponseData = string.IsNullOrEmpty(routeName) ? "NA" : routeName;
                }
                else
                {
                    responseModel.IsSuccess = false;
                    responseModel.Message = "Transport name not found";
                    responseModel.ResponseData = "NA";
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetTransportNameById", ex.Message + " | " + ex.StackTrace);
                responseModel.IsSuccess = false;
                responseModel.Message = "Error occurred while getting transport name by ID";
                responseModel.ResponseData = "NA";
            }

            return responseModel;
        }


        public async Task<ResponseModel> getTransportList(string clientId)
        {
            try
            {
                string formattedEndpoint =string.Format( ProxyConstant.TransportGetTransportList_GetTransportList,15);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "getTransportList", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting transport list.", ex);
            }
        }

        public async Task<ResponseModel> GetStopListWithLatLong(string routeId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportGetStopListWithLatLong_GetStopListWithLatLong,16, routeId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    transport_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );
                if (response.IsSuccess && response.ResponseData != null)
                {
                    // Deserialize ResponseData into LoginResponseDto
                    var transportlist = JsonConvert.DeserializeObject<List<TransportDTO>>(
                response.ResponseData.ToString()
                    );

                    // Optional: attach parsed object back to response (for downstream use)
                    response.ResponseData = transportlist;
                }
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "GetStopListWithLatLong", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while getting stop list with lat long.", ex);
            }
        }

        public async Task<ResponseModel> deleteTransport(string routeId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportDelete_DeleteTransport, routeId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "deleteTransport", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting transport.", ex);
            }
        }

        public async Task<ResponseModel> DeleteBusStop(string busStopId, string clientId)
        {
            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.TransportDeleteBusStop_DeleteBusStop, busStopId);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    student_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Delete,
                    null
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportClient", "DeleteBusStop", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while deleting bus stop.", ex);
            }
        }
    }
}
