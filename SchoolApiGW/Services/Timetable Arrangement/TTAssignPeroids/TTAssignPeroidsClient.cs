using SchoolApiGW.Helper;
using SchoolApiGW.Services.Salary;

namespace SchoolApiGW.Services.Timetable_Arrangement.TTAssignPeroids
{
    public class TTAssignPeroidsClient : ProxyBaseUrl, ITTAssignPeroidsClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public TTAssignPeroidsClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
    }
}
