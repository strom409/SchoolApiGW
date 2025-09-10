using SchoolApiGW.Helper;
using SchoolApiGW.Services.Salary;

namespace SchoolApiGW.Services.Timetable_Arrangement.TimeTableHistory
{
    public class TimeTableHistoryClient : ProxyBaseUrl, ITimeTableHistoryClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public TimeTableHistoryClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
    }
}
