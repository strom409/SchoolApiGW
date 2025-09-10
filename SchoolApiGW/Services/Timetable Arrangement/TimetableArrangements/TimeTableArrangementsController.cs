using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApiGW.Services.Timetable_Arrangement.TimetableArrangements
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableArrangementsController : ControllerBase
    {
        private readonly ITimeTableArrangementsClient _timeTableArrangementsClient;

        public TimeTableArrangementsController(ITimeTableArrangementsClient timeTableArrangementsClient)
        {
            _timeTableArrangementsClient = timeTableArrangementsClient;
        }
    }
    

}
