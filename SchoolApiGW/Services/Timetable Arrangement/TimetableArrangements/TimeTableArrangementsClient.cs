using SchoolApiGW.Helper;
using SchoolApiGW.Services.Salary;

namespace SchoolApiGW.Services.Timetable_Arrangement.TimetableArrangements
{
    public class TimeTableArrangementsClient : ProxyBaseUrl, ITimeTableArrangementsClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public TimeTableArrangementsClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
    }
}