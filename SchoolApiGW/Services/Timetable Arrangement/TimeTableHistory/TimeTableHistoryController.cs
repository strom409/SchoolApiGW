using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApiGW.Services.Timetable_Arrangement.TimeTableHistory
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableHistoryController : ControllerBase
    {
        private readonly ITimeTableHistoryClient _timeTableHistoryClient;

        public TimeTableHistoryController(ITimeTableHistoryClient timeTableHistoryClient)
        {
            _timeTableHistoryClient = timeTableHistoryClient;
        }
    }
}
