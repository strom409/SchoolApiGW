using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApiGW.Services.Timetable_Arrangement.TTDays
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTDaysController : ControllerBase
    {
        private readonly ITTDaysClient _ttDaysClient;

        public TTDaysController(ITTDaysClient ttDaysClient)
        {
            _ttDaysClient = ttDaysClient;
        }

    }
}
