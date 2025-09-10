using SchoolApiGW.Helper;
using SchoolApiGW.Services.Salary;

namespace SchoolApiGW.Services.Timetable_Arrangement.TTDays
{
    public class TTDaysClient : ProxyBaseUrl, ITTDaysClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public TTDaysClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
    }
}
