using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SchoolApiGW.Helper;
using System.Net.Http;

namespace SchoolApiGW.Services.EmpStatus
{
    public class EmpStatusClient:  ProxyBaseUrl, IEmpStatusClient
    {
        private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public EmpStatusClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        : base(configuration)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseModel> GetEmployeeStatus(string clientId)
        {
    

            try
            {
                string formattedEndpoint = string.Format(ProxyConstant.Employee_GetEmployeeStatus);


                var response = await ApiHelper.ApiConnection<ResponseModel>(
                    _httpClientFactory,
                    employee_Universal_API_Host,
                    formattedEndpoint,
                    HttpMethod.Get,
                    null
                );

                if (response.IsSuccess && response.ResponseData != null)
                {
                    var statusList = JsonConvert.DeserializeObject<List<EmployeeStatus>>(response.ResponseData.ToString());
                    response.ResponseData = statusList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("EmployeeClient", "GetEmployeeStatus", ex.Message + " | " + ex.StackTrace);
                throw new ApplicationException("Error occurred while fetching employee statuses.", ex);
            }
        }

    }
}
