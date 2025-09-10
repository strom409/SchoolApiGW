using SchoolApiGW.Helper;
using SchoolApiGW.Services.Salary;

namespace SchoolApiGW.Services.Timetable_Arrangement.TTPeroids
{
    public class TTPeroidClient : ProxyBaseUrl, ITTPeroidClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public TTPeroidClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base(configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
    }
}
