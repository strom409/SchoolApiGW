using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Services.OptionalMarks;

namespace SchoolApiGW.Services.Units
{
    public class UnitsClient : ProxyBaseUrl, IUnitsClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public UnitsClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> AddUnit(UnitDto unit, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Unit_Add;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Post,
                    unit
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UnitClient", "AddUnit", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while adding unit.", ex);
            }
        }

        public async Task<ResponseModel> GetAllUnits(string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Unit_GetAll;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var list = JsonConvert.DeserializeObject<List<UnitDto>>(response.ResponseData.ToString());
                    response.ResponseData = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UnitClient", "GetAllUnits", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching all units.", ex);
            }
        }

        public async Task<ResponseModel> GetUnitById(string? param, string clientId)
        {
            try
            {
                string endpoint = string.Format(ProxyConstant.Unit_GetById, param);
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var unit = JsonConvert.DeserializeObject<UnitDto>(response.ResponseData.ToString());
                    response.ResponseData = unit;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UnitClient", "GetUnitById", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching unit by ID.", ex);
            }
        }

        public async Task<ResponseModel> UpdateUnit(UnitDto unit, string clientId)
        {
            try
            {
                string endpoint = ProxyConstant.Unit_Update;
                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    Examination_Universal_API_Host,
                    endpoint,
                    HttpMethod.Put,
                    unit
                );
                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("UnitClient", "UpdateUnit", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while updating unit.", ex);
            }
        }
    }
}
